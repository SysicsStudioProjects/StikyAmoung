using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelected : MonoBehaviour
{
    //hide in inspector;

    public Skin GlassesSkin;
    public Skin HatSkin;

    public List<GameObject> GlasesObject;
    public List<GameObject> HatObject;

    public Skins skins;

    private void OnEnable()
    {
        init();
        EventController.useSkin += SetSkin;
    }
    private void OnDisable()
    {
        EventController.useSkin -= SetSkin;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void init()
    {
        foreach (var item in skins.allSkins)
        {
            switch (item.type)
            {
                case SkinType.hat:
                    if (item.state == SkinState.useIt)
                    {
                        HatSkin = item;
                        SetupSkin(HatObject,HatSkin);
                    }
                    break;
                case SkinType.bette:
                    break;
                case SkinType.skin:
                    break;
                case SkinType.glasse:
                    if (item.state==SkinState.useIt)
                    {
                        GlassesSkin = item;
                        SetupSkin(GlasesObject,GlassesSkin);
                    }
                    break;
                case SkinType.wap:
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void SetSkin(Skin s)
    {
        switch (s.type)
        {
            case SkinType.hat:
                HatSkin = s;
                SetupSkin(HatObject,HatSkin);
                break;
            case SkinType.bette:
                break;
            case SkinType.skin:
                break;
            case SkinType.glasse:
                GlassesSkin = s;
                SetupSkin(GlasesObject,GlassesSkin);
                break;
            case SkinType.wap:
                break;
            default:
                break;
        }
        // GlassesSkin = s;

    }

    void SetupSkin(List<GameObject> objects,Skin s)
    {
        foreach (var item in objects)
        {
            if (item.name==s.name)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

}
