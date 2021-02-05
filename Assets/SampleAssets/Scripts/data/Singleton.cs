
using UnityEngine;
using UnityEngine.Rendering;
public class Singleton : MonoBehaviour
{
    public static Singleton _instance;
    #region data
    public int level;
    public int coins;
    
    public bool sound;
    public Skins skins;
    #endregion

    #region save&load
    public void save()
    {
        SaveLoad.save(this);
        load();
    }
    public void load()
    {
        GeneralPlayerData data = SaveLoad.load();
        if (data != null)
        {
            level = data.level;
            coins = data.coins;
            sound = data.sound;
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

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // When the Menu starts, set the rendering to target 20fps
        OnDemandRendering.renderFrameInterval = 3;


        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            
            Destroy(this);
        }
        
    }


   

    public void Update()
    {
       /* if (Input.GetMouseButton(0) || (Input.touchCount > 0))
        {
            
            // If the mouse button or touch detected render at 60 FPS (every frame).
            OnDemandRendering.renderFrameInterval = 1;
            Application.targetFrameRate = 30;
        }
        else
        {
            // If there is no mouse and no touch input then we can go back to 20 FPS (every 3 frames).
            OnDemandRendering.renderFrameInterval = 3;
            Application.targetFrameRate = 30;

        }*/
    }
    // Update is called once per frame
    
}
