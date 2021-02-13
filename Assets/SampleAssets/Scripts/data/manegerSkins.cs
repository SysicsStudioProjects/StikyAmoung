
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class manegerSkins : MonoBehaviour
{
    public Skins skins;
    public GameObject player,skinPrefab;
    public Transform parent, parentBuy;
    public delegate void ChangeSkinStat(Skin s);
    public static ChangeSkinStat changeSkinStat;

    public List<Button> buttons;
    public List<Button> buttonsBuy;
    public List<GameObject> p;
    public Sprite EnabledSprite;
    public Sprite DisabledSprite;
    public Text CoinsText;

    public Scrollbar bar;

    public GameObject EndSkinScene;
    public List<ImageModel> imageModels;
    
    private void Start()
    {
        
        CoinsText.text = Singleton._instance.coins.ToString();
       
    }

    private void OnEnable()
    {
        listSkin("hat");
    }

    public void skinStart()
    {
        listSkin("hat");
        // player.transform.position = posShop.position;
        //player.SetActive(true);
    }

    public void skinEnd()
    {
        //player.transform.position = posStart.position;
        player.SetActive(false);
    }

    public void buyStart()
    {
        listBuy("hat");
        //player.SetActive(false);
    }

    public void buyEnd()
    {
        //player.SetActive(true);
    }
    public void listSkin(string type)
    {
        bar.value = 0;
        /* foreach (Button b in buttons)
         {
             if (b.name == type + "Button")
                 b.gameObject.GetComponent<Image>().sprite = EnabledSprite;
             else
                 b.gameObject.GetComponent<Image>().sprite = DisabledSprite;
         }*/

        foreach (var item in buttons)
        {
            int index = imageModels.FindIndex(d => d.type == item.name );
            item.GetComponent<Image>().sprite = imageModels[index].DisableImage;
            if (item.name== type + "Button")

            {
                item.GetComponent<Image>().sprite = imageModels[index].enableImage;
            }
            
        }

      //  posWatch();
        listVide();
        foreach(Skin s in skins.allSkins)
        {
            if (s.type.ToString() == type )
            {
                GameObject child = Instantiate(skinPrefab, parent);
                /*if (s.toWatch) {
                    child.transform.SetAsFirstSibling();
                }*/
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
        /*foreach (Skin s in skins.allSkins)
        {
            if (s.type.ToString() == type && s.state == SkinState.none)
            {
                GameObject child = Instantiate(skinPrefab, parentBuy);
                
                child.GetComponent<SkinStart>().setSkin(s);

            }
        }*/
        for (int i = 0; i < skins.allSkins.Count; i++)
        {
            print(i);
            if (skins.allSkins[i].type.ToString() == type )
            {
                GameObject child = Instantiate(skinPrefab, parentBuy);

                child.GetComponent<SkinStart>().setSkin(skins.allSkins[i]);

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
        /*foreach (Skin s in skins.allSkins)
        {
            if (s.toWatch)
            {
                foreach(GameObject g in p)
                {
                    g.SetActive(g.name == s.type.ToString());
                }
            }
        }*/
    }
    bool gamestarted;
    public void LoadScene()
    {
        gamestarted=true;
        //  SceneManager.LoadScene(SceneName);
        if (EventController.gameStart!=null)
        {
            EventController.gameStart(gamestarted);
        }
        this.gameObject.SetActive(false);
    }

   

    public void LoadPlayScene(){
        Singleton._instance.LoadPlayScene();
        EndSkinScene.SetActive(true);
    }
}

[System.Serializable]
public class ImageModel
{
    public string type;
    public Sprite enableImage;
    public Sprite DisableImage;
}