using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : IPersistentSingleton<BattleStarter>
{
    public BattleType[] potentialBattles;
    private bool inArea;
    public bool activateOnStay;
    public float timeBetweenBattles;
    private float betweenBattleCounter;

    void Start()
    {
        betweenBattleCounter = Random.Range(timeBetweenBattles*.5f, timeBetweenBattles*1.5f);
    }

    void Update()
    {
        if(inArea && Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            if(PlayerController.Instance.canMove){
                betweenBattleCounter -= Time.deltaTime;
            }

            if(betweenBattleCounter <= 0)
            {
                betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
                GameManager.Instance.battleActive = true;
                Debug.Log("Batalha encontrada");
                StartCoroutine(StartBattleCo());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inArea = true;   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
        }
    }
    
    public IEnumerator StartBattleCo()
    {
        UIFade.Instance.FadeToBlack();
        int selectedBattle = Random.Range(0, potentialBattles.Length);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("inimigo: " + potentialBattles[selectedBattle].enemies[0]);
        BattleManager.Instance.BattleStart(potentialBattles[selectedBattle].enemies);
        UIFade.Instance.FadeFromBlack();
    }
}
