using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSpecialController : MonoBehaviour
{
    public List<SpecialCharacter> specialCharacters;
    public RawImage CharacterImage;
    public Image Round;

    public GameObject MainMenu;

      Skin specialSkin;

    public static Skin skinSetter;
    public MainMenu mainMenu;
    public Button RewardButton;
    public GameControl gameControl;
    // Start is called before the first frame update

    private void OnEnable()
    {
        skinSetter = null;
        RewardButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
        EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        EventController.videoRewarded += VideoBonuseRewarded;
        SetCharacter();
    }

    public void SetCharacter()
    {
        specialSkin = SpecialItemProgress.skin;
        for (int i = 0; i < specialCharacters.Count; i++)
        {
            if (specialSkin.name==specialCharacters[i].name)
            {
                CharacterImage.texture = specialCharacters[i].texture;
                return;
            }
        }
    }
    private void OnDisable()
    {
        EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        EventController.videoRewarded -= VideoBonuseRewarded;
    }
    // Update is called once per frame
    void Update()
    {
        Round.transform.Rotate(Vector3.forward, 0.1f);
    }

    
    public void NoThanksButton(string s)
    {
        if (GameControl.IsRewardedAfterWin==false)
        {
            AdsManager._instance.ShowIntertiate(s);
        }
        gameControl.LoadScene();

        //this.gameObject.SetActive(false);
        // MainMenu.SetActive(true);

    }

    public void SetSpecialPackage()
    {
        
        //this.gameObject.SetActive(false);
        IsBonuseReward = true;
        AdsManager._instance.ShowRewardVideo("Startlevel_Special_Try_reward");
    }

    void ChangeRewardStatut(bool b)
    {
        RewardButton.interactable = b;
       
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
           // EventController.getSpecialPackage(specialSkin);
            /*  if (EventController.getSpecialPackage != null)
              {
                  EventController.getSpecialPackage(specialSkin);
              }
              mainMenu.SatrtGame();*/
            skinSetter = specialSkin;
            gameControl.LoadScene();
            
        }

    }


}

[System.Serializable]
public class SpecialCharacter
{
    public string name;
    public Texture texture;
}