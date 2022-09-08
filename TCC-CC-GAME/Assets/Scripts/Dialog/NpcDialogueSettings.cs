using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGTALK;

public class NpcDialogueSettings : MonoBehaviour
{

    [SerializeField] private Image npcImage;
    [SerializeField] private Sprite npcSprite;
    [SerializeField] private bool playerRange = false;
    [SerializeField] private bool isInDialogue = false;
    [SerializeField] private string startLineDialogueNpc, endLineDialogueNpc;
    [SerializeField] private GameObject dialogueIcon;
    
    private RPGTalk rpgTalk;

    // Start is called before the first frame update

    private void Awake()
    {
        rpgTalk = FindObjectOfType<RPGTalk>();
        rpgTalk.OnEndTalk += RpgTalk_OnEndTalk;
    }

    private void RpgTalk_OnEndTalk()
    {
        isInDialogue = false;
        GameManager.Instance.dialogActive = false;
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && playerRange && !isInDialogue)
        {
            rpgTalk.NewTalk(startLineDialogueNpc, endLineDialogueNpc);
            isInDialogue = true;
            GameManager.Instance.dialogActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                npcImage.sprite = npcSprite;
                playerRange = true;
                dialogueIcon.SetActive(true);
                break;

        }
    }

     void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                playerRange = false;
                dialogueIcon.SetActive(false);
                break;
        }   
    }

}
