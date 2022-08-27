using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : IPersistentSingleton<BattleManager>
{

    private bool battleActive;
    private bool ableToAct;
    public bool turnWaiting;

    public GameObject battleScene;
    public GameObject uiButtonsHolder;

    public int chanceToFlee = 35;
    public int currentEnemy;
    public int numberOfRounds = 5;
    public int currentRound;
    private int correctAnswersCount = 0;

    public Transform playerPositions;
    public Transform[] enemyPosition;

    public BattleChar playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers;

    public Text playerNameText;
    public string corretAnswer = "10";
    public Text playerAnswer;

    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void BattleStart(string[] enemiesToSpaw)
    {
        if (!battleActive)
        {
            activeBattlers = new List<BattleChar>();
            battleActive = true;
            GameManager.Instance.battleActive = true;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            battleScene.SetActive(true);
            BattleChar newPlayer = Instantiate(playerPrefabs, playerPositions.position, playerPositions.rotation);

            newPlayer.transform.parent = playerPositions;
            playerAnimator = newPlayer.GetComponent<Animator>();
            activeBattlers.Add(newPlayer);

            for (int i = 0; i < enemiesToSpaw.Length; i++)
            {
                if (enemiesToSpaw[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpaw[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[0], enemyPosition[0].transform.position, enemyPosition[0].transform.rotation);
                            newEnemy.transform.parent = enemyPosition[0];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }
            UpdateUIStats();
            ableToAct = true;
            currentEnemy = 0;
            turnWaiting = true;
            currentRound = 0;
            correctAnswersCount = 0;
        }
    }

    public void NextTurn()
    {
        currentRound++;
        Debug.Log("Round: " + currentRound);
        turnWaiting = true;
        UpdateUIStats();
        ableToAct = true;
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
        StartCoroutine(AnswerAnimation());
    }

    public IEnumerator AnswerAnimation()
    {
        yield return new WaitForSeconds(1f);
        NextTurn();
        AnswerQuestion();
    }

    public IEnumerator EndBattle()
    {
        battleActive = false;
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
        UpdateUIStats();
        if(currentRound == numberOfRounds)
        {
            ValidateWin();
        }
        
    }

    public void UpdateUIStats()
    {
        //playerHpText.text = activeBattlers[0].currentHp.ToString() + "/" + activeBattlers[0].maxHp.ToString();
        //playerMpText.text = activeBattlers[0].currentMp.ToString() + "/" + activeBattlers[0].maxMp.ToString();
    }

    public void ValidateWin()
    {
        if (correctAnswersCount < 3)
        {
            activeBattlers[0].EnemyFade();
            Debug.Log("Você perdeu a batalha");
            StartCoroutine(EndBattle());
        }
        if(correctAnswersCount >= 3)
        {
            Debug.Log("Você venceu a batalha");
            activeBattlers[1].EnemyFade();
            activeBattlers.RemoveAt(1);
            currentEnemy++;
        }
        if (activeBattlers.Count == 1)
        {
            StartCoroutine(EndBattle());
        }
    }

    public void ValidateCorretAnswer()
    {
        if (playerAnswer.text == corretAnswer)
        {
            correctAnswersCount++;
            Debug.Log("Acertou a resposta");
        }
        else
        {
            Debug.Log("Errou a resposta");
        }
    }

}
