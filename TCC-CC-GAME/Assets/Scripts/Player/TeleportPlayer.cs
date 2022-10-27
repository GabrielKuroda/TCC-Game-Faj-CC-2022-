using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform teleportPoint;
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
                Teleport(teleportPoint.position);
                break;
        }
    }

    public static void Teleport(Vector3 teleportPoint)
    {
        PlayerController.Instance.transform.position = teleportPoint;
    }
}
