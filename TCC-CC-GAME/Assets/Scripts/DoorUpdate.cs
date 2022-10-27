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
        Debug.Log(collision.tag);
        switch (collision.tag)
        {
            case "Player":
                PlayerPrefs.SetInt(variableDoorName, 1);
                Debug.Log("variableDoorName value = " + PlayerPrefs.GetInt("forest_door_2_is_open"));
                break;
        }
    }

}
