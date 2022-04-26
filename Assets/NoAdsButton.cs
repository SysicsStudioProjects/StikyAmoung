using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAdsButton : MonoBehaviour
{
   public void ShowInter()
    {
        AdsManager._instance.ShowIntertiate("DefaultInterstitial");
    }
}
