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
    public Image LookImage;

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
       //EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        manegerSkins.changeSkinStat += setUpStat;
        EventController.videoRewarded += VideoBonuseRewarded;
      //  _watchButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
    }
    private void OnDisable()
    {
        //EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
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
        _watchButton.interactable = AdsManager._instance.VerifRewarded();
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
        else if (skin.state==SkinState.Lock)
        {
            LookImage.gameObject.SetActive(true);
        }
    }
    public void watch()
    {
        ShowVideo();
        
    }

    public void use()
    {
        for (int i = 0; i < Singleton._instance.skins.allSkins.Count; i++)
        {
            if (skin.type == SkinType.special&&skin.state!=SkinState.Lock)
            {
                if (Singleton._instance.skins.allSkins[i].state == SkinState.useIt && Singleton._instance.skins.allSkins[i].type != SkinType.glasse && Singleton._instance.skins.allSkins[i].type != SkinType.bette)
                {
                    print("iss meeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
                    Singleton._instance.skins.allSkins[i].state = SkinState.buyIt;
                }
            }
            else if (skin.type != SkinType.glasse&&skin.type!=SkinType.bette)
            {
                if (Singleton._instance.skins.allSkins[i].state == SkinState.useIt && Singleton._instance.skins.allSkins[i].type == SkinType.special && skin.state != SkinState.Lock)
                {
                    print("iss Thereeeeeeeeeee");

                    Singleton._instance.skins.allSkins[i].state = SkinState.buyIt;
                }
            }
            /*if (Singleton._instance.skins.allSkins[i].state==SkinState.useIt&& Singleton._instance.skins.allSkins[i].type!=SkinType.glasse&& Singleton._instance.skins.allSkins[i]!=skin&&skin.type==SkinType.special)
            {
                Singleton._instance.skins.allSkins[i].state = SkinState.buyIt;
            }*/

        }
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


       /* if (s.type==SkinType.glasse)
        {
            if (skin.type==SkinType.glasse&&skin==s)
            {

                return;
            }
            else
            {
                if (skin.state==SkinState.useIt&& skin.type == SkinType.glasse)
                {
                    skin.state = SkinState.buyIt;
                    setButton();
                }
            }
        }
        else
        {
            if (skin.state==SkinState.useIt&&skin.type!=SkinType.glasse)
            {
                if (skin==s)
                {
                    return;
                }
                else
                {
                    skin.state = SkinState.buyIt;
                    setButton();
                    
                }
            }

        }*/
        if (s.type!=SkinType.special&&s.type!=SkinType.glasse)
        {
            print("we are herreeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            print(skin.type);
            print(skin.state);

            if (skin.type==SkinType.special&& skin.state != SkinState.none&&skin.state!=SkinState.Lock)
            {
               int index= Singleton._instance.skins.allSkins.FindIndex(d => d.name == skin.name);
                
                if (index!=-1)
                {
                  
                    Singleton._instance.skins.allSkins[index].state = SkinState.buyIt;
                    Singleton._instance.save();
                }
                skin.state = SkinState.buyIt;
                setButton();
                return;
            }
        }
        if (s.type != skin.type&&s.type!=SkinType.special)
        {
            return;
        }

        else if (s.type == SkinType.special)
        {
            if (skin.type!=SkinType.glasse)
            {
                if (s != skin && skin.state != SkinState.none && skin.state != SkinState.Lock)
                {
                    int index = Singleton._instance.skins.allSkins.FindIndex(d => d.name == skin.name);
                    if (index != -1)
                    {
                        Singleton._instance.skins.allSkins[index].state = SkinState.buyIt;
                        Singleton._instance.save();
                    }
                    skin.state = SkinState.buyIt;
                    setButton();
                }
            }
        }
        else
        {
            if (s != skin && skin.state!=SkinState.none && skin.state != SkinState.Lock)
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
        //AdsManager._instance.ShowRewardVideo("Shop_item_reward");
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
