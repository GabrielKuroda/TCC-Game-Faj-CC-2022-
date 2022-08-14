using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialsLoader : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject uiScreen;
    public GameObject player;

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
