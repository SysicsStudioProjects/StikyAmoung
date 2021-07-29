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
            if (item.type==SkinType.special)
            {
                item.state = SkinState.Lock;
                item.nbWatch = 24;
                item.toWatch = false;
                item.inProgress = false;
            }
            else
            {
                item.nbWatch = (int)(item.price / 1500);
                if (item.nbWatch<=1)
                {
                    item.nbWatch = 2;
                }
                item.toWatch = true;
                item.state = SkinState.none;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
