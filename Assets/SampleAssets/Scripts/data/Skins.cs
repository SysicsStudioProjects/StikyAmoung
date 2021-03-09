using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinState{
    none,buyIt,useIt
}

public enum SkinType
{
    hat,bette,skin,glasse,wap,special
}

[System.Serializable]
public class Skin
{
    public string name;
    public SkinState state;
    public int price;
    public SkinType type;
    public Sprite icon;
    public bool toWatch;
    public int nbWatch;
    public bool inProgress;
}

[CreateAssetMenu(fileName ="skins",menuName ="skins")]
public class Skins : ScriptableObject
{
    public List<Skin> allSkins;
   
    
}
