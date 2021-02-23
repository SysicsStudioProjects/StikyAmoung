using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoosePanelController : MonoBehaviour
{
    public Button SkipLevel;
    bool IsBonuseReward;

    public GameControl gameControl;

    private void OnEnable()
    {
        SkipLevel.interactable = IronSource.Agent.isRewardedVideoAvailable();
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        EventController.videoRewarded += VideoBonuseRewarded;
    }

    private void OnDisable()
    {
        EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        EventController.videoRewarded -= VideoBonuseRewarded;
    }

    void ChangeRewardStatut(bool b)
    {
        SkipLevel.interactable = b;
    }

    void VideoBonuseRewarded(bool b)
    {
        if (b == false)
        {
            return;
        }
        if (IsBonuseReward)
        {
            gameControl.LoadScene();
        }

    }

    public void OnclickSkipLevel()
    {
        AdsManager._instance.ShowRewardVideo();
        IsBonuseReward = true;
    }
}
