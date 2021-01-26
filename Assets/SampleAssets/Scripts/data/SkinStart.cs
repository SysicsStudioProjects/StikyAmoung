using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinStart : MonoBehaviour
{
    public Skin skin;

    public GameObject useButton, watchButton, buyButton;
    public Image image;
    public Text nbWatch;

    public void setSkin(Skin s) {
        skin = s;
        
        image.sprite = s.icon;
        nbWatch.text = s.nbWatch + "";
        setButton();
    }
    private void OnEnable()
    {
        manegerSkins.changeSkinStat += setUpStat;

    }
    private void OnDisable()
    {
        manegerSkins.changeSkinStat -= setUpStat;
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
            buyButton.SetActive( false);
        }
        else if (skin.state == SkinState.none)
        {
            
            useButton.SetActive (false);
            watchButton.SetActive( false);
            buyButton.SetActive( true);
        }
        else if (skin.state == SkinState.useIt)
        {
            
            useButton.SetActive( false);
            watchButton.SetActive( false);
            buyButton.SetActive( false);
        }
        else if (skin.state == SkinState.buyIt)
        {
            
            useButton.SetActive( true);
            watchButton.SetActive( false);
            buyButton.SetActive( false);
        }
    }
    public void watch()
    {
        skin.nbWatch -= 1;
        nbWatch.text = skin.nbWatch+"";
        if(skin.nbWatch==0)
        {
            skin.state = SkinState.buyIt;
            skin.toWatch = false;
            setButton();
        }
        Singleton._instance.save();
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
        skin.state = SkinState.buyIt;
        if (manegerSkins.changeSkinStat != null)
        {
            manegerSkins.changeSkinStat(skin);
            setButton();
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

}
