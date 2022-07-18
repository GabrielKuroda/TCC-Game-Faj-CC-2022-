using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialsLoader : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject uiScreen;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        if (UIFade.Instance != null)
        {
            UIFade.Instance = Instantiate(uiScreen).GetComponent<UIFade>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
