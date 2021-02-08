
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class ProgressManager : MonoBehaviour
{
    public Skins skins;

    public Image ItemSkin;
    public Image SlideItemSkin;
    public Text NbVideo;

    private void OnEnable()
    {
        ShowItem();
    }

    void ShowItem()
    {
        SetupItem();
    }
    void SetupItem()
    {
        Skin cUrrentProgres = SetSkin();
        if (cUrrentProgres!=null)
        {
            ItemSkin.sprite = cUrrentProgres.icon;
            SlideItemSkin.sprite = cUrrentProgres.icon;
            SlideItemSkin.fillAmount = 1.0f / (float)cUrrentProgres.nbWatch;
           /* switch (cUrrentProgres.nbWatch)
            {
                case 5:
                    SlideItemSkin.fillAmount = 0.2f;
                    break;
                case 4:
                    SlideItemSkin.fillAmount = 0.4f;
                    break;
                case 3:
                    SlideItemSkin.fillAmount = 0.6f;
                    break;
                case 2:
                    SlideItemSkin.fillAmount = 0.8f;
                    break;
                case 1:
                    SlideItemSkin.fillAmount = 1.0f;
                    cUrrentProgres.inProgress = false;
                    break;
                default:
                    break;
            }*/
            NbVideo.text = cUrrentProgres.nbWatch.ToString();

        }
    }

    public void WatchVideo()
    {

    }

    Skin SetSkin()
    {
        Skin a=null;
        foreach (var item in skins.allSkins)
        {
            if (item.state==SkinState.none)
            {
                if (item.inProgress && item.nbWatch > 1)
                {
                    a = item;
                    a.nbWatch--;
                    return a;

                }
            }
           
        }
            
                foreach (var s in skins.allSkins)
                {
                    if (s.state==SkinState.none&&s.nbWatch>=2)
                    {
                        a = s;
                        s.inProgress = true;
                        return a;
                    }
                }

            

        
        if (a==null)
        {
            LoadNextScene();
            this.gameObject.SetActive(false);
            
        }
        return a;
    }

    void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;

        if (buildIndex == SceneManager.sceneCountInBuildSettings-1)
        {
            buildIndex = 0;
            SceneManager.LoadScene(buildIndex);
        }
        else
        {
            Singleton._instance.level = buildIndex + 1;
            Singleton._instance.save();
            SceneManager.LoadScene(buildIndex + 1);
            
        }
    }

    
}
