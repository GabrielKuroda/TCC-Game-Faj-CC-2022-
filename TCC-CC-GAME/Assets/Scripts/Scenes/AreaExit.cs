using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance theEntrance;

    public float waitToLoad = 1f;

    private bool shouldLoadAfterFade;

    // Start is called before the first frame update

    private void Awake()
    {
        theEntrance = GetComponentInChildren<AreaEntrance>();
        theEntrance.transitionName = areaTransitionName;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shouldLoadAfterFade = true;
            GameManager.Instance.fadingBetweenAreas = true;
            UIFade.Instance.FadeToBlack();
            PlayerController.Instance.areaTransitionName = areaTransitionName;
        }
    }
}
