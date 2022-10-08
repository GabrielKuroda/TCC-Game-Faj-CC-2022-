using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class BattleStarter : IPersistentSingleton<BattleStarter>
{
    public BattleType[] potentialBattles;
    private bool inArea;
    public bool activateOnStay;
    public float timeBetweenBattles;
    private float betweenBattleCounter;

    public string operation;
    public string difficult;
    
    private string endpointBase = "http://localhost:8080/questions";

    void Start()
    {
        betweenBattleCounter = Random.Range(timeBetweenBattles*.5f, timeBetweenBattles*1.5f);
        
    }

    void Update()
    {
        if(inArea && Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            if(PlayerController.Instance.CanMove){
                betweenBattleCounter -= Time.deltaTime;
            }

            if(betweenBattleCounter <= 0)
            {
                betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
                GameManager.Instance.battleActive = true;
                Debug.Log("Batalha encontrada");
                string endpoint = endpointBase + "/filter/" + difficult + "/" + operation;
                StartCoroutine(StartBattleCo(endpoint));
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
    
    public IEnumerator StartBattleCo(string endpoint)
    {

        var getRequest = CreateRequest(endpoint);
        yield return getRequest.SendWebRequest();
        QuestionList questionList = QuestionMapper.convertJsonToQuestion(getRequest.downloadHandler.text);

        UIFade.Instance.FadeToBlack();
        int selectedBattle = Random.Range(0, potentialBattles.Length);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("inimigo: " + potentialBattles[selectedBattle].enemies[0]);
        BattleManager.Instance.BattleStart(potentialBattles[selectedBattle].enemies, questionList, difficult, operation);
        UIFade.Instance.FadeFromBlack();
    }

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }
}
