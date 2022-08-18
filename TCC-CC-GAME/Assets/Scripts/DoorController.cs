using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private GameObject doorArea;
    [SerializeField] private Sprite spriteOpenDoor;
    [SerializeField] private string variableDoorName;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.GetInt(variableDoorName) == 1)
        {
            _spriteRenderer.sprite = spriteOpenDoor;
            doorArea.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
