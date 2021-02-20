using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LevelManager : MonoBehaviour
{
    public GameObject Level;
    public GameObject SkinMenu;
    public GameObject Player;
    public CinemachineBrain cinemachineBrain;
    void Awake(){
       
      
    }
    private void Start()
    {
       
    }
    private void OnEnable()
    {
        Time.timeScale = 1;
        SkinMenu.SetActive(false);
        StartCoroutine(EnablePlayer());
        IronSource.Agent.loadInterstitial();
    }

    IEnumerator EnablePlayer(){
        yield return new WaitForSeconds(0.2f);
        Level.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Player.SetActive(true);
    }

    public void OpenSkinMenu(){
         Level.SetActive(false);
        SkinMenu.SetActive(true);
         StartCoroutine(ControlCamera());
    }

    public void CloseSkinMenu(){
         Level.SetActive(true);
        SkinMenu.SetActive(false);
        StartCoroutine(ControlCamera());
    }

    IEnumerator ControlCamera(){
        cinemachineBrain.m_DefaultBlend.m_Time = 0.4f;
        yield return new WaitForSeconds(0.2f);
        cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;

    }
    
}
