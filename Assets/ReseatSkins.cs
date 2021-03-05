using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseatSkins : MonoBehaviour
{
    public Skins skins;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in skins.allSkins)
        {
            item.nbWatch = 5;
            item.toWatch = true;
            item.state = SkinState.none;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
