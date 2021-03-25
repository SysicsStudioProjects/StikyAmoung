using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestAds : MonoBehaviour
{

    public Text AdsText;
    public Text AdsState;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventController.videoRewarded+=GetRewarded;
    }

    private void OnDisable()
    {
        EventController.videoRewarded-=GetRewarded;
        
    }

    // Update is called once per frame
    void Update()
    {
        //AdsState.text=AdsManager._instance.AdsState;
    }

    public void ShowRewardVideo(){
       // AdsManager._instance.ShowRewardVideo();
    }

    void GetRewarded(bool b){
        if (b==true){
            int a=int.Parse(AdsText.text);
            a++;
            AdsText.text=a.ToString();
        }
    }
}
