using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : IPersistentSingleton<UIManager>
{

    [SerializeField] private GameObject panelExit;
    [SerializeField] private bool _painelExitIsOpen = false;

    public bool PainelExitIsOpen { get => _painelExitIsOpen; set => _painelExitIsOpen = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape) && _painelExitIsOpen) || (Input.GetKeyDown(KeyCode.Escape) && !_painelExitIsOpen))
        {
            _painelExitIsOpen = !_painelExitIsOpen;
            panelExit.SetActive(_painelExitIsOpen);

        }    
    }


}
