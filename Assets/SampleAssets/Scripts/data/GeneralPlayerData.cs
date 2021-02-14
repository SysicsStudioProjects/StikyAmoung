using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class GeneralPlayerData 
{
    public int level;
    public int coins;
    public bool sound;
    public int AdsSpinVideo;
    public string spinDate;
    public DateTime AdsspinDate;
    //public Skins skins;
    public List<int> state;
    public List<bool> toWatch;
    public List<int> nbWatch;

    public GeneralPlayerData(Singleton singleton)
    {
        level = singleton.level;
        coins = singleton.coins;
        sound = singleton.sound;
        AdsSpinVideo = singleton.AdsSpinVideo;
        AdsspinDate = singleton.AdsspinDate;
        spinDate = singleton.spinDate;
        // skins = singleton.skins;
        state = new List<int>();
        toWatch = new List<bool>();
        nbWatch = new List<int>();
        foreach (Skin s in singleton.skins.allSkins)
        {
            state.Add((int)s.state);
            toWatch.Add(s.toWatch);
            nbWatch.Add(s.nbWatch);
        }
    }
}
