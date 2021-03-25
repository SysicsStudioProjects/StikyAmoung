using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMessageController : MonoBehaviour
{
    public GameObject MessagePanel;
    public GameObject GreatJob;
    bool isFirstKill;

    private void Awake()
    {
        PlayerEvents.canKill = false;
    }
    private void OnEnable()
    {
        PlayerEvents.canKill = false;
    }
    private void Update()
    {
        if (isFirstKill==true)
        {
            return;
        }
        if (PlayerEvents.canKill==true)
        {
            MessagePanel.SetActive(true);
            Time.timeScale = 0;
           
        }
        if ((Input.GetMouseButtonUp(0)))
        {
            if (MessagePanel.activeInHierarchy)
            {
                Time.timeScale = 1;
                MessagePanel.SetActive(false);
                GreatJob.SetActive(true);
                isFirstKill = true;
            }
           

        }
    }
}
