using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPanelController : MonoBehaviour
{
    public Skins skins;

    [HideInInspector]
    public Skin GlassesSkin;
    [HideInInspector]
    public Skin WeopenSkin;
    [HideInInspector]
    public Skin HatSkin;

    public Image GlassesImage;
    public Image WeopenImage;
    public Image HatImage;

    public GameObject MainMenu;

    public Button[] buttons;

    private void OnEnable()
    {
      
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = IronSource.Agent.isRewardedVideoAvailable();
        }
        MainMenu.SetActive(false);
        GlassesSkin = ChooseElement(SkinType.glasse);
        WeopenSkin = ChooseElement(SkinType.wap);
        HatSkin = ChooseElement(SkinType.hat);
        InitImage();

        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        EventController.videoRewarded += VideoBonuseRewarded;

    }

    private void OnDisable()
    {
        EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        EventController.videoRewarded -= VideoBonuseRewarded;
    }

    Skin ChooseElement(SkinType type)
    {
        List<Skin> s = new List<Skin>();
        int j = 0;
        for (int i = 0; i < skins.allSkins.Count; i++)
        {
            if (skins.allSkins[i].type==type&& skins.allSkins[i].state!=SkinState.useIt)
            {
                s.Add(skins.allSkins[i]);
            }
        }

        int Randomindex = Random.Range(0, s.Count);

        return s[Randomindex];


    }
    // Start is called before the first frame update
   void InitImage()
    {
        GlassesImage.sprite = GlassesSkin.icon;
        WeopenImage.sprite = WeopenSkin.icon;
        HatImage.sprite = HatSkin.icon;
    }


    string switchSkin;
    public void Onclick(string t)
    {
        switchSkin = t;
        IsBonuseReward = true;
        AdsManager._instance.ShowRewardVideo("Startlevel_itemtry_reward");
    }

    void ChangeRewardStatut(bool b)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = b;
        }
    }

    bool IsBonuseReward;
    void VideoBonuseRewarded(bool b)
    {
        if (b == false)
        {
            return;
        }
        if (IsBonuseReward)
        {
            Rewarded(switchSkin);
            MainMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }

    void Rewarded(string t)
    {
        switch (t)
        {
            case "hat":
                if (EventController.getSkinstart != null)
                {
                    EventController.getSkinstart(HatSkin);
                }
                break;


            case "glasse":
                if (EventController.getSkinstart != null)
                {
                    EventController.getSkinstart(GlassesSkin);
                }
                break;
            case "wap":
                if (EventController.getSkinstart != null)
                {
                    EventController.getSkinstart(WeopenSkin);
                }
                break;
            default:
                break;
        }
        IsBonuseReward = false;
    }
}
