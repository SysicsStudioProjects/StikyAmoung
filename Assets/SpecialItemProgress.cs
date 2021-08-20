using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpecialItemProgress : MonoBehaviour
{
    public Skins Skin;

    public Image IconItem;
    public Image BarItem;
    public static Skin skin;
    public static bool Topen;
    public BGSpecialController Bgprogress;
    public GameControl gameControl;

    public GameObject BonusePanel;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Topen = false;
        skin = null;
        Verif();
        startNewItem();
        Desactivate();
        if (skin!=null)
        {
            Setup();
        }

    }

    void Verif()
    {
        foreach (var item in Skin.allSkins)
        {
            if (item.inProgress && item.toWatch == false&& item.type==SkinType.special&& item.state == SkinState.Lock)
            {
                skin = item;
                return;
            }
        }
    }

    void startNewItem()
    {
        if (skin == null)
        {
            foreach (var item in Skin.allSkins)
            {
                if (!item.inProgress && item.toWatch == false&& item.type == SkinType.special&&item.state==SkinState.Lock)
                {
                    skin = item;
                    item.inProgress = true;
                    return;
                }
            }
        }
    }

    void Desactivate()
    {
        if (skin==null)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Setup()
    {
     
        IconItem.sprite = skin.icon;
        BarItem.sprite = skin.icon;
        switch (skin.nbWatch)
        {
            case 9:
                skin.nbWatch = 8;
                BarItem.fillAmount = 0.0f;
                StartCoroutine(animeBar(0.25f));
                //BarItem.fillAmount = 0.25f;
                Singleton._instance.save();
                break;
            case 8:
                skin.nbWatch = 7;
                BarItem.fillAmount = 0.25f;
                StartCoroutine(animeBar(0.25f));
               // BarItem.fillAmount = 0.5f;
                Singleton._instance.save();
                break;
            case 7:
                skin.nbWatch = 6;
                BarItem.fillAmount = 0.5f;
                StartCoroutine(animeBar(0.25f));
                // BarItem.fillAmount = 0.75f;
                Singleton._instance.save();
                break;
            case 6:
                skin.nbWatch = 5;
                BarItem.fillAmount = 0.75f;
                StartCoroutine(animeBar(0.25f));
                // BarItem.fillAmount = 1.0f;
                skin.toWatch = true;
                skin.inProgress = false;
                skin.state = SkinState.none;
                Topen = true;
                Singleton._instance.save();
                break;
            default:
                break;
        }
    }
    public void OpenBgprogress()
    {
        if (Topen==true)
        {
            BonusePanel.SetActive(false);
            Bgprogress.gameObject.SetActive(true);
           // Bgprogress.SetCharacter(skin.name);
        }
        else
        {
            gameControl.LoadScene();   
        }
    }

    IEnumerator animeBar(float j)
    {
        for (int i = 0; i < 100; i++)
        {
            BarItem.fillAmount += j / 100.0f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
