using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IPersistentSingleton<GameManager>
{

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, battleActive;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas || battleActive )
        {
            PlayerController.Instance.CanMove = false;
        }
        else
        {
            PlayerController.Instance.CanMove = true;
        }
    }
}
