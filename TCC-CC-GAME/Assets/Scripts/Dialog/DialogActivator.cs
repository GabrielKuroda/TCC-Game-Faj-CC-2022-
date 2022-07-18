using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines;

    private bool canActivate;

    public bool isPerson = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.Instance.dialogBox.activeInHierarchy && !GameManager.Instance.gameMenuOpen)
        {
            DialogManager.Instance.ShowDialog(lines, isPerson);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Verifica se o objeto que entrou ? o Player
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Verifica se o objeto que saiu ? o Player
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
