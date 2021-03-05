using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class SkinStart : MonoBehaviour
{
    public Skin skin;

    public GameObject useButton, watchButton, buyButton,removeButton;
    public Image image;
    public Text nbWatch;
    public Text Price;
    int pric;
    public Button _watchButton;

    public PlayableDirector playableDirector;

    public void setSkin(Skin s) {
        skin = s;
        
        image.sprite = s.icon;
        nbWatch.text = s.nbWatch + "";
        Price.text = s.price.ToString();
        pric = s.price;
        setButton();
    }
    private void OnEnable()
    {
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        manegerSkins.changeSkinStat += setUpStat;
        EventController.videoRewarded += VideoBonuseRewarded;
        _watchButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
    }
    private void OnDisable()
    {
        EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        manegerSkins.changeSkinStat -= setUpStat;
        EventController.videoRewarded -= VideoBonuseRewarded;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setButton()
    {
        if (skin.toWatch)
        {
            useButton.SetActive( false);
            watchButton.SetActive( true);
            buyButton.SetActive( true);
            removeButton.SetActive(false);
        }
        else if (skin.state == SkinState.none)
        {
            
            useButton.SetActive (false);
            watchButton.SetActive( false);
            buyButton.SetActive( true);
            removeButton.SetActive(false);
        }
        else if (skin.state == SkinState.useIt)
        {
            
            useButton.SetActive( false);
            watchButton.SetActive( false);
            buyButton.SetActive( false);
            removeButton.SetActive(true);
        }
        else if (skin.state == SkinState.buyIt)
        {
            
            useButton.SetActive( true);
            watchButton.SetActive( false);
            buyButton.SetActive( false);
            removeButton.SetActive(false);
        }
    }
    public void watch()
    {
        ShowVideo();
        
    }

    public void use()
    {
        print(skin.name);
        skin.state = SkinState.useIt;
        if (manegerSkins.changeSkinStat != null)
        {
            manegerSkins.changeSkinStat(skin);
            setButton();
        }
        if (EventController.useSkin!=null)
        {
            EventController.useSkin(skin);
        }
        Singleton._instance.save();
    }

    public void buy()
    {
        if (pric>Singleton._instance.coins)
        {
            playableDirector.Play();
            return;
        }
        Singleton._instance.coins -= pric;
        skin.state = SkinState.buyIt;
        skin.toWatch = false;
        skin.inProgress = false;
        setButton();
       /* if (manegerSkins.changeSkinStat != null)
        {
            manegerSkins.changeSkinStat(skin);
           
        }*/
        Singleton._instance.save();
    }

    public void RemoveSkin()
    {
        skin.state = SkinState.buyIt;
        if (manegerSkins.changeSkinStat != null)
        {
            manegerSkins.changeSkinStat(skin);
            setButton();
        }

        if (EventController.removeSkin!=null)
        {
            EventController.removeSkin(skin);
        }
        Singleton._instance.save();
    }

    void setUpStat(Skin s) {
        if (s.type != skin.type)
        {
            return;
        }
        else
        {
            if (s != skin && skin.state!=SkinState.none)
            {
                skin.state = SkinState.buyIt;
                setButton();
            }
            
        }
    }

    public void SwitchSkin()
    {
        if (EventController.switchKin!=null)
        {
            EventController.switchKin(skin);
        }
    }

    public void ChangeRewardStatut(bool b)
    {
        _watchButton.interactable = b;
    }

    bool IsWatched;
    void ShowVideo()
    {
        IsWatched = true;
        AdsManager._instance.ShowRewardVideo("Shop_item_reward");
    }

    void VideoBonuseRewarded(bool b)
    {
        if (b == false)
        {
            IsWatched = false;
            return;
        }
        if (IsWatched)
        {
            skin.nbWatch -= 1;
            nbWatch.text = skin.nbWatch + "";
            if (skin.nbWatch == 0)
            {
                skin.state = SkinState.buyIt;
                skin.toWatch = false;
                skin.inProgress = false;
                setButton();
            }
            Singleton._instance.save();

        }
        IsWatched = false;

    }

}
