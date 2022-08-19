using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string variableDoorName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                PlayerPrefs.SetInt(variableDoorName, 1);
                break;
        }
    }

}
