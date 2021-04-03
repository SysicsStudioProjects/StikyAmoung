using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingMenuController : MonoBehaviour
{

    public Sprite enblesound;
    public Sprite disablesound;

    public Image ButtonImage;
    private void OnEnable()
    {
        Time.timeScale = 0;
        InitSprite();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

    }

    public void OnclickButton()
    {
        bool b = Singleton._instance.sound;
        Singleton._instance.sound = !b;
        Singleton._instance.save();
        InitSprite();
    }

    void InitSprite()
    {
        if (Singleton._instance.sound==true)
        {
            ButtonImage.sprite = enblesound;
        }
        else
        {
            ButtonImage.sprite = disablesound;

        }
    }

    public void RateUs()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.sysics.rocket.dark.impostor");
#elif UNITY_IPHONE
 Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
#endif
    }
}
