using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingCanvas : MonoBehaviour
{

    public float PlayerSpeed;
    public float EnnemieDetectTime;
    public bool autoFocuse;
    public bool vibration;

    public Text PlayerSpeedText;
    public Text AlertTimeText;


    public Slider SpeedSlider;
    public Slider DetectTime;

    public Toggle autoTogle;
    public Toggle vibrationTogle;

    private void OnEnable()
    {
       // PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("speed"))
        {
            PlayerPrefs.SetFloat("speed", PlayerSpeed);
        }

        if (!PlayerPrefs.HasKey("detect"))
        {
            PlayerPrefs.SetFloat("detect", EnnemieDetectTime);
        }
        PlayerSpeed = PlayerPrefs.GetFloat("speed");
        EnnemieDetectTime = PlayerPrefs.GetFloat("detect");
        SpeedSlider.value = PlayerSpeed;
        DetectTime.value = EnnemieDetectTime;
        int auto = PlayerPrefs.GetInt("auto");
        if (auto==-1)
        {
            autoFocuse = false;
           
        }
        else
        {
            autoFocuse = true;
        }
        autoTogle.isOn = autoFocuse;
        int _vibration = PlayerPrefs.GetInt("vibration");
        if (_vibration == -1)
        {
            vibration = false;
        }
        else
        {
            vibration = true;
        }
        vibrationTogle.isOn = vibration;
    }

    public void Onsppedchange(Slider slider)
    {
        PlayerSpeed = slider.value;
        PlayerSpeedText.text = PlayerSpeed.ToString();
        PlayerPrefs.SetFloat("speed", PlayerSpeed);

    }

    public void OnAlertchange(Slider slider)
    {
        EnnemieDetectTime = slider.value;
        AlertTimeText.text = EnnemieDetectTime.ToString();
        PlayerPrefs.SetFloat("detect", EnnemieDetectTime);

    }

    public void OnchangeFocuseToogle(Toggle t)
    {
        autoFocuse = t.isOn;
        if (autoFocuse)
        {
            PlayerPrefs.SetInt("auto", 1);
        }
        else
        {
            PlayerPrefs.SetInt("auto", -1);

        }
    }

    public void OnchangeVibrationToogle(Toggle t)
    {
        vibration = t.isOn;
        if (vibration)
        {
            PlayerPrefs.SetInt("vibration",1) ;
        }
        else
        {
            PlayerPrefs.SetInt("vibration", -1);

        }
    }

    public void OkButon()
    {
        if (EventController.sendSettingData!=null)
        {
            EventController.sendSettingData(PlayerSpeed, EnnemieDetectTime, autoFocuse, vibration);
        }
    }
}
