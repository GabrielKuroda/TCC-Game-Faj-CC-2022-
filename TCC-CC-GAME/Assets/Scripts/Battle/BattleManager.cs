using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleManager : IPersistentSingleton<BattleManager>
{
    private bool battleActive;
    private bool ableToAct;
    public bool turnWaiting;

    public GameObject battleScene;
    public GameObject answerPanel;
    public GameObject uiButtonsHolder;

    public int chanceToFlee = 35;
    public int currentEnemy;
    public int numberOfRounds = 5;
    private int currentRound = 1;
    private int correctAnswersCount = 0;
    private int index = 0;

    public Transform playerPositions;
    public Transform[] enemyPosition;

    public BattleChar playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers;

    public QuestionList questionList;
    public QuestionList battleQuestionList;

    private string correctAnswer;
    public  TextMeshProUGUI question;
    public Text playerAnswer;
    public Text battleStageInfo;
    public Text difficultInfo;
    public Text calcTypeInfo;
    public Text localInfo;
    public Text lifesRemainingText;

    public InputField answerInput;

    private string difficult;
    private string operation;

    private Animator playerAnimator;

    public Vector3 respawnLocal;

    // Start is called before the first frame update
    void Start()
    {
        respawnLocal = new Vector3(-49.5f, 7.4f, 0f);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (battleActive)
        {
            if (turnWaiting)
            {
                if (ableToAct)
                {
                    uiButtonsHolder.SetActive(true);
                }
                else
                {
                    uiButtonsHolder.SetActive(false);
                }
            }
            else
            {
                uiButtonsHolder.SetActive(false);
            }
        }

        if(GameManager.Instance.lifes == 0 && !battleActive)
        {
            StartCoroutine(RespawnDungeon());
        }
    }

    public void BattleStart(string[] enemiesToSpaw, QuestionList questions, string receivedDifficult, string receivedOperation)
    {
        if (!battleActive)
        {

            if(receivedOperation == "multiplicacao")
            {
                for (int i = 0; i < questions.questions.Count; i++)
                {
                    questions.questions[i].equation = questions.questions[i].equation.Replace("*", "x");
                }
            }

            activeBattlers = new List<BattleChar>();
            questionList = SelectRandomQuestions(questions);
            currentRound = 1;
            index = 0;
            Debug.Log("Questão 1: " + questionList.questions[0].equation);
            Debug.Log("Resposta 1: " + questionList.questions[0].answer);
            Debug.Log("Questão 2: " + questionList.questions[1].equation);
            Debug.Log("Resposta 2: " + questionList.questions[1].answer);
            Debug.Log("Questão 3: " + questionList.questions[2].equation);
            Debug.Log("Resposta 3: " + questionList.questions[2].answer);
            Debug.Log("Questão 4: " + questionList.questions[3].equation);
            Debug.Log("Resposta 4: " + questionList.questions[3].answer);
            Debug.Log("Questão 5: " + questionList.questions[4].equation);
            Debug.Log("Resposta 5: " + questionList.questions[4].answer);
            battleActive = true;
            GameManager.Instance.battleActive = true;
            difficult = receivedDifficult;
            operation = receivedOperation;
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            battleScene.SetActive(true);

            for (int i = 0; i < enemiesToSpaw.Length; i++)
            {
                if (enemiesToSpaw[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpaw[i])
                        {
                            Debug.Log("Achei o caba aqui meu");
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPosition[i].transform.position, enemyPosition[i].transform.rotation);
                            newEnemy.transform.parent = enemyPosition[0];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
            UpdateUIStats();
            answerPanel.SetActive(false);
            ableToAct = true;
            currentEnemy = 0;
            turnWaiting = true;
            correctAnswersCount = 0;
        }
    }

    private QuestionList SelectRandomQuestions(QuestionList questionList)
    {
        List<QuestionObject> questionObjects = new List<QuestionObject>();
        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, questionList.questions.Count - 1);
            questionObjects.Add(questionList.questions[index]);
            questionList.questions.RemoveAt(index);
        }
        QuestionList finalQuestionList = new QuestionList();
        finalQuestionList.questions = questionObjects;
        return finalQuestionList;
    }

    public void NextTurn()
    {
        currentRound++;
        UpdateUIStats();
        Debug.Log("Round: " + currentRound);
        turnWaiting = true;
        ableToAct = true;
        answerPanel.SetActive(false);
    }

    public void Flee()
    {
        int fleeSuccess = Random.Range(0, 100);
        if (fleeSuccess < chanceToFlee)
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            Debug.Log("Não pode escapar da batalha");
            currentRound++;
        }
    }

    public void Answer()
    {
        ableToAct = false;
        answerPanel.SetActive(false);
        StartCoroutine(AnswerAnimation());
    }

    public void ActivateAnswerPanel()
    {
        answerPanel.SetActive(true);
    }

    public IEnumerator AnswerAnimation()
    {
        yield return new WaitForSeconds(1f);
        AnswerQuestion();
        if (currentRound != numberOfRounds)
        {
            NextTurn();
        }
    }

    public IEnumerator RespawnDungeon()
    {
        GameManager.Instance.lifes = 3;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Desert Store");
        TeleportPlayer.Teleport(respawnLocal);
        index = 0;
        UpdateUIStats();
    }

    public IEnumerator EndBattle()
    {
        battleActive = false;
        lifesRemainingText.text = GameManager.Instance.lifes.ToString();
        yield return new WaitForSeconds(.5f);
        UIFade.Instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        battleScene.SetActive(false);
        UIFade.Instance.FadeFromBlack();
        currentRound = 0;
        GameManager.Instance.battleActive = false;
        Destroy(activeBattlers[0].gameObject);
    }

    public IEnumerator EnemyTurn()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void AnswerQuestion()
    {
        ValidateCorretAnswer();
        if(currentRound == numberOfRounds)
        {
            ValidateWin();
        }
    }

    public void UpdateUIStats()
    {
        correctAnswer = questionList.questions[index].answer;
        question.text = questionList.questions[index].equation + " = ?";
        difficult = questionList.questions[index].difficulty;
        operation = questionList.questions[index].operation;
        battleStageInfo.text = currentRound + " / " + numberOfRounds;
        difficultInfo.text = questionList.questions[index].difficulty;
        calcTypeInfo.text = questionList.questions[index].operation;
        localInfo.text = "Dungeon";
        index++;
        lifesRemainingText.text = GameManager.Instance.lifes.ToString();
    }

    public void ValidateWin()
    {
        if (correctAnswersCount < 3)
        {
            activeBattlers[0].EnemyFade();
            Debug.Log("Você perdeu a batalha");
            GameManager.Instance.lifes--;
            StartCoroutine(EndBattle());
        }
        if(correctAnswersCount >= 3)
        {
            Debug.Log("Você venceu a batalha");
            activeBattlers[0].EnemyFade();
            currentEnemy++;
        }
        if (activeBattlers.Count == 1)
        {
            StartCoroutine(EndBattle());
        }
    }

    public void ValidateCorretAnswer()
    {
        Debug.Log("resposta do player = " + playerAnswer.text);
        Debug.Log("resposta correta = " + correctAnswer);
        if (playerAnswer.text == correctAnswer)
        {
            correctAnswersCount++;
            Debug.Log("Acertou a resposta");
        }
        else
        {
            Debug.Log("Errou a resposta");
        }
        answerInput.text = "";
    }
}
