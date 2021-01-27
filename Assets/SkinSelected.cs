using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelected : MonoBehaviour
{
    //hide in inspector;

    public Skin GlassesSkin;
    public Skin HatSkin;
    public Skin MaterialSkin;
    public List<GameObject> GlasesObject;
    public List<GameObject> HatObject;
    public List<SkinnedMeshRenderer> SkinsObjetcs;

    public List<Material> UsefulMaterial;
    public Skins skins;

    public List<GameObject> Weopen;
    public Skin WeopenSkin;

    public List<GameObject> Pet;
    public Skin PetSkin;

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
                    if (item.state == SkinState.useIt)
                    {
                        PetSkin = item;
                        SetupSkin(Pet, PetSkin);
                    }
                    break;
                case SkinType.skin:
                    if (item.state==SkinState.useIt)
                    {
                        MaterialSkin = item;
                        SetupMaterial();
                    }
                    break;
                case SkinType.glasse:
                    if (item.state==SkinState.useIt)
                    {
                        GlassesSkin = item;
                        SetupSkin(GlasesObject,GlassesSkin);
                    }
                    break;
                case SkinType.wap:
                    if (item.state == SkinState.useIt)
                    {
                        WeopenSkin = item;
                        SetupSkin(Weopen, WeopenSkin);
                    }
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

    void SetupMaterial()
    {
        int index = UsefulMaterial.FindIndex(d => d.name == MaterialSkin.name);
        foreach (var item in SkinsObjetcs)
        {
            if (index!=-1)
            {
                item.material = UsefulMaterial[index];
            }
        }
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
                PetSkin = s;
                SetupSkin(Pet, PetSkin);
                break;
            case SkinType.skin:
                
                    MaterialSkin = s;
                    SetupMaterial();
                
                break;
            case SkinType.glasse:
                GlassesSkin = s;
                SetupSkin(GlasesObject,GlassesSkin);
                break;
            case SkinType.wap:
                WeopenSkin = s;
                SetupSkin(Weopen, WeopenSkin);
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
