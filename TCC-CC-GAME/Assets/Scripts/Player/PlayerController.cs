using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IPersistentSingleton<PlayerController>
{
    public Vector3 bottomLeftLimit;
    public Vector3 topRightLimit;

    public bool canMove = true;

    public string areaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }
}
