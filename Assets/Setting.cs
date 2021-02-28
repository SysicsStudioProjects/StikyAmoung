using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class Setting : MonoBehaviour
{
    public GameObject GlobalVolume;
   public void OnchangeFPS(Slider s)
    {
        Application.targetFrameRate = (int)s.value;
    }

    public void OnchangeRenderingFrame(Slider s)
    {
        OnDemandRendering.renderFrameInterval = (int)s.value;
    }
    
    public void EnablePostProcessing(Toggle t)
    {
        GlobalVolume.SetActive(t.isOn);
    }


}
