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
    public string scene;
    
    private string endpointBase = "http://ec2-18-228-8-216.sa-east-1.compute.amazonaws.com:8080/questions";

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
        //object objResponse = "[{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"3+2\",\"answer\":\"5\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"4+4\",\"answer\":\"8\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"14+2\",\"answer\":\"16\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"3+3\",\"answer\":\"6\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"3+4\",\"answer\":\"7\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"5+3\",\"answer\":\"8\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"3+6\",\"answer\":\"9\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"10+1\",\"answer\":\"11\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"9+8\",\"answer\":\"17\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"2+18\",\"answer\":\"20\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"5+1\",\"answer\":\"6\"},{\"difficulty\":\"facil\",\"operation\":\"adicao\",\"equation\":\"15+8\",\"answer\":\"23\"}]";
        //QuestionList questionList = QuestionMapper.convertJsonToQuestion(objResponse.ToString());


        UIFade.Instance.FadeToBlack();
        int selectedBattle = Random.Range(0, potentialBattles.Length);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("inimigo: " + potentialBattles[selectedBattle].enemies[0]);
        BattleManager.Instance.BattleStart(potentialBattles[selectedBattle].enemies, questionList, difficult, operation, scene);
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
