using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionName == PlayerController.Instance.areaTransitionName)
        {
            GameManager.Instance.lifes = 3;
            PlayerController.Instance.transform.position = transform.position;
        }

        UIFade.Instance.FadeFromBlack();
        GameManager.Instance.fadingBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
