
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
public class Singleton : MonoBehaviour
{
    public static Singleton _instance;
    #region data
    public int level;
    public int coins;
    public int AdsSpinVideo;
    public DateTime AdsspinDate;
    public string spinDate;
    
    
    public bool sound;
    public Skins skins;
    #endregion

    static int currentsceneIndex;
    #region save&load
    public void save()
    {
        SaveLoad.save(this);
        load();
        if (EventController.onchangeItems!=null)
        {
            EventController.onchangeItems();
        }
    }
    public void load()
    {
        GeneralPlayerData data = SaveLoad.load();
        if (data != null)
        {
            level = data.level;
            coins = data.coins;
            sound = data.sound;
            spinDate = data.spinDate;
            AdsSpinVideo = data.AdsSpinVideo;
            AdsspinDate = data.AdsspinDate;
            //skins = data.skins;
            
            if (data.nbWatch != null)
            {
                for (int i = 0; i < data.nbWatch.Count; i++)
                {
                    skins.allSkins[i].state = (SkinState)data.state[i];
                    skins.allSkins[i].toWatch = data.toWatch[i];
                    skins.allSkins[i].nbWatch = data.nbWatch[i];
                }
            }
        }
    }
    #endregion
    private void OnEnable()
    {
        
        
    }

    void Awake()
    {
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 40;
        // When the Menu starts, set the rendering to target 20fps
        //OnDemandRendering.renderFrameInterval = 3;


        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            
            Destroy(gameObject);
        }
        bool verif = SaveLoad.verifPath();
        if (verif)
        {
            load();

        }
        else
        {
            save();
        }

    }

    public void LoadSkinSelectedMenu(int indexCurrentScene,int indexSkinScene){
        currentsceneIndex=indexCurrentScene;
        print(currentsceneIndex);
        LoadScene(indexSkinScene);
    }

    public void LoadPlayScene(){
        
       StartCoroutine(LoadYourAsyncScene(currentsceneIndex));
        print(currentsceneIndex);

    }
   
   void LoadScene(int index){
      
       StartCoroutine(LoadYourAsyncScene(index));
   }

    
  IEnumerator LoadYourAsyncScene(int index)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    // Update is called once per frame
    
}
