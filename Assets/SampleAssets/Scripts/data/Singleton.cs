using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        load();
    }

    void Awake()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
