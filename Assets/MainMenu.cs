using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public Text CoinsText;

    private void OnEnable()
    {
        InitCoins();
        EventController.onchangeItems += InitCoins;
    }

    private void OnDisable()
    {
        EventController.onchangeItems -= InitCoins;

    }

    void Start(){
       
    }

    public void SatrtGame(){
         if (EventController.gameStart!=null)
        {
            EventController.gameStart(true);
        }
        this.gameObject.SetActive(false);
    }

     public void LoadSkinScene(){
        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
          Singleton._instance.LoadSkinSelectedMenu(buildIndex,1);
    }

    void InitCoins()
    {
        CoinsText.text = Singleton._instance.coins.ToString();
    }
}
