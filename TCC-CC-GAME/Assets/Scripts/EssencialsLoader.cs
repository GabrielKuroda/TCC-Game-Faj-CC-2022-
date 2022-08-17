using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialsLoader : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject uiScreen;
    public GameObject player;
    public GameObject battleManager;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = Instantiate(gameManager).GetComponent<GameManager>();
        }
        if (PlayerController.Instance == null)
        {
            PlayerController.Instance = Instantiate(player).GetComponent<PlayerController>();
        }
        if (UIFade.Instance == null)
        {
            UIFade.Instance = Instantiate(uiScreen).GetComponent<UIFade>();
        }
        if (BattleManager.Instance == null)
        {
            BattleManager.Instance = Instantiate(battleManager).GetComponent<BattleManager>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
