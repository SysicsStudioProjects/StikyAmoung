using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manegerSkins : MonoBehaviour
{
    public Skins skins;
    public GameObject player,skinPrefab;
    public Transform parent,parentBuy,posShop,posStart;
    public delegate void ChangeSkinStat(Skin s);
    public static ChangeSkinStat changeSkinStat;

    public List<Button> buttons;
    public List<Button> buttonsBuy;
    public List<GameObject> p;
    public Sprite EnabledSprite;
    public Sprite DisabledSprite;
    private void Start()
    {
        
    }

    public void skinStart()
    {
        listSkin("hat");
        // player.transform.position = posShop.position;
        player.SetActive(true);
    }

    public void skinEnd()
    {
        //player.transform.position = posStart.position;
        player.SetActive(false);
    }

    public void buyStart()
    {
        listBuy("hat");
        player.SetActive(false);
    }

    public void buyEnd()
    {
        player.SetActive(true);
    }
    public void listSkin(string type)
    {
        foreach (Button b in buttons)
        {
            if (b.name == type + "Button")
                b.gameObject.GetComponent<Image>().sprite = EnabledSprite;
            else
                b.gameObject.GetComponent<Image>().sprite = DisabledSprite;
        }
        posWatch();
        listVide();
        foreach(Skin s in skins.allSkins)
        {
            if (s.type.ToString() == type &&s.state!=SkinState.none)
            {
                GameObject child = Instantiate(skinPrefab, parent);
                if (s.toWatch) {
                    child.transform.SetAsFirstSibling();
                }
                child.GetComponent<SkinStart>().setSkin(s);
                
            }
        }
    }
    

    private void listVide()
    {
        for(int i=0; i<parent.childCount; i++ )
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void listBuy(string type)
    {
        foreach (Button b in buttonsBuy)
        {
            if (b.name == type + "Button")
                b.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            else
                b.gameObject.GetComponent<Image>().color = Color.white;
        }

        listVideBuy();
        foreach (Skin s in skins.allSkins)
        {
            if (s.type.ToString() == type && s.state == SkinState.none)
            {
                GameObject child = Instantiate(skinPrefab, parentBuy);
                
                child.GetComponent<SkinStart>().setSkin(s);

            }
        }
    }

    private void listVideBuy()
    {
        for (int i = 0; i < parentBuy.childCount; i++)
        {
            Destroy(parentBuy.GetChild(i).gameObject);
        }
    }

    public void posWatch()
    {
        foreach (Skin s in skins.allSkins)
        {
            if (s.toWatch)
            {
                foreach(GameObject g in p)
                {
                    g.SetActive(g.name == s.type.ToString());
                }
            }
        }
    }
}

