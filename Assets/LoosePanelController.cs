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
            Singleton._instance.level += 1;
            Singleton._instance.save();
            gameControl.LoadScene();
        }

    }

    public void OnclickSkipLevel()
    {
        AdsManager._instance.ShowRewardVideo("Endlevel_fail_skiplevel_reward");
        IsBonuseReward = true;
    }
}
