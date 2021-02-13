
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class ProgressManager : MonoBehaviour
{
    public Skins skins;

    public Image ItemSkin;
    public Image SlideItemSkin;
    public Text NbVideo;
    public Button _watchButton;
    Skin cUrrentProgres;
    public Button UseButton;
    private void OnEnable()
    {
        ShowItem();
        EventController.videoRewarded += VideoBonuseRewarded;
        _watchButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
    }
    private void OnDisable()
    {
        EventController.videoRewarded += VideoBonuseRewarded;
        _watchButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
    }

    void ShowItem()
    {
        SetupItem();
       
    }
    void SetupItem()
    {
         cUrrentProgres = SetSkin();
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
        ShowVideo();
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
                    Singleton._instance.save();
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
                        Singleton._instance.save();
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
    bool IsWatched;
    void ShowVideo()
    {
        IsWatched = true;
        AdsManager._instance.ShowRewardVideo();
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
            cUrrentProgres.nbWatch -= 1;
            NbVideo.text = cUrrentProgres.nbWatch + "";
            if (cUrrentProgres.nbWatch == 0)
            {
                cUrrentProgres.state = SkinState.buyIt;
                cUrrentProgres.toWatch = false;
                cUrrentProgres.inProgress = false;
                UseButton.gameObject.SetActive(true);
                _watchButton.gameObject.SetActive(false);
                //setButton();
            }
            Singleton._instance.save();

        }
        IsWatched = false;

    }
    public void ChangeRewardStatut(bool b)
    {
        _watchButton.interactable = b;
    }

    public void UseSKin()
    {
        cUrrentProgres.state = SkinState.useIt;
        Singleton._instance.save();

    }
}
