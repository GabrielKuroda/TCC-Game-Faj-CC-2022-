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

    public int currentTurn;
    public int chanceToFlee = 35;
    public int currentEnemy;

    public Transform playerPositions;
    public Transform[] enemyPosition;

    public BattleChar playerPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers;

    public Text playerNameText;
    public Text corretAnswer;
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
                if (activeBattlers[currentTurn].isPlayer)
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
                    StartCoroutine(EnemyTurn());
                }
            }
        }
    }

    public void BattleStart(string[] enemiesToSpaw)
    {
        if (!battleActive)
        {
            activeBattlers = new List<BattleChar>();
            battleActive = true;
            PlayerController.Instance.canMove = false;

            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);

            battleScene.SetActive(true);
            Debug.Log("pos: " + playerPositions.position);
            Debug.Log("rot: " + playerPositions.rotation);
            Debug.Log("playr prefab: " + playerPrefabs);
            /*BattleChar newPlayer = Instantiate(playerPrefabs, playerPositions.position, playerPositions.rotation);

            newPlayer.transform.parent = playerPositions;
            playerAnimator = newPlayer.GetComponent<Animator>();
            activeBattlers.Add(newPlayer);
            playerNameText.text = activeBattlers[0].charName;

            for (int i = 0; i < enemiesToSpaw.Length; i++)
            {
                if (enemiesToSpaw[i] != "")
                {
                    for (int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if (enemyPrefabs[j].charName == enemiesToSpaw[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPosition[i].transform.position, enemyPosition[i].transform.rotation);
                            newEnemy.transform.parent = enemyPosition[i];
                            activeBattlers.Add(newEnemy);
                        }
                    }
                }
            }*/
            UpdateUIStats();
            ableToAct = true;
            currentEnemy = 0;
            turnWaiting = true;
            currentTurn = 0;
        }
    }

    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
            ableToAct = true;
        }
        Debug.Log(currentTurn);
        turnWaiting = true;
        UpdateUIStats();
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
            currentTurn++;
        }
    }

    public void Attack()
    {
        ableToAct = false;
        StartCoroutine(AttackAnimation());
    }

    public IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(1f);
        AnswerQuestion(1);
        NextTurn();
    }

    public IEnumerator EndBattle()
    {
        battleActive = false;
        yield return new WaitForSeconds(.5f);
        UIFade.Instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        battleScene.SetActive(false);
        UIFade.Instance.FadeFromBlack();
        currentTurn = 0;
        PlayerController.Instance.canMove = true;
        Destroy(activeBattlers[0].gameObject);
    }

    public IEnumerator EnemyTurn()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        NextTurn();
    }

    public void AnswerQuestion(int target)
    {
        ValidateCorretAnswer();
        UpdateUIStats();
        ValidateWin();
    }

    public void UpdateUIStats()
    {
        //playerHpText.text = activeBattlers[0].currentHp.ToString() + "/" + activeBattlers[0].maxHp.ToString();
        //playerMpText.text = activeBattlers[0].currentMp.ToString() + "/" + activeBattlers[0].maxMp.ToString();
    }

    public void ValidateWin()
    {
        if (activeBattlers[0].correctAnswers < 3)
        {
            activeBattlers[0].EnemyFade();
            Debug.Log("Você perdeu a batalha");
            StartCoroutine(EndBattle());
        }
        if(activeBattlers[0].correctAnswers >= 3)
        {
            Debug.Log("Você venceu a batalha");
            activeBattlers[1].EnemyFade();
            activeBattlers.RemoveAt(1);
            currentEnemy++;
        }
        if (activeBattlers.Count == 1)
        {
            Debug.Log("Você venceu a batalha");
            StartCoroutine(EndBattle());
        }
    }

    public void ValidateCorretAnswer()
    {
        if (playerAnswer == corretAnswer)
        {
            activeBattlers[0].correctAnswers++;
        }
    }

}
