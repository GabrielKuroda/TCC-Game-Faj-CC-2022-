using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IPersistentSingleton<GameManager>
{

    public bool gameMenuOpen;
    public bool dialogActive;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
