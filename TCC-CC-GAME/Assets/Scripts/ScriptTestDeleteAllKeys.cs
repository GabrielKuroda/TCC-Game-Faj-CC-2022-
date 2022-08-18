using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTestDeleteAllKeys : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        PlayerPrefs.DeleteAll();

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
