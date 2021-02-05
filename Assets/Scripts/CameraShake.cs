using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtual;
    CinemachineBasicMultiChannelPerlin cinemachineBasic;



    private void OnEnable()
    {
        EventController.startKillEvent += Kill;    
    }
    private void OnDisable()
    {
        EventController.startKillEvent -= Kill;

    }
    // Start is called before the first frame update
    void Start()
    {
        cinemachineBasic = cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
   void Kill()
    {
        StartCoroutine(startShake());
      

    }

    IEnumerator startShake()
    {
        cinemachineBasic.m_AmplitudeGain = LeanTween.linear(0, 1, 1f);
        yield return new WaitForSeconds(0.5f);
        cinemachineBasic.m_AmplitudeGain = LeanTween.linear(1, 0, 1f);
    }
}
