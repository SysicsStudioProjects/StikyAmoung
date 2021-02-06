using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkinSelected : MonoBehaviour
{
    public Skin GlassesSkin;
    public Skin HatSkin;
    public Skin MaterialSkin;
    public List<GameObject> GlasesObject;
    public List<GameObject> HatObject;
    public List<SkinnedMeshRenderer> SkinsObjetcs;

    public List<Material> UsefulMaterial;
    public Material DefaultSkin;
    public Skins skins;

    public List<GameObject> Weopen;
    public Skin WeopenSkin;

    public List<GameObject> Pet;
    public Skin PetSkin;
    // Start is called before the first frame update

    private void OnEnable()
    {
        EventController.switchKin += ChangeSkin;
    }
    private void OnDisable()
    {
        EventController.switchKin -= ChangeSkin;
        CloseALL();

    }
    public void ChangeSkin(Skin s)
    {
        switch (s.type)
        {
            case SkinType.hat:
                HatSkin = s;
                SetupSkin(HatObject, HatSkin);
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
                SetupSkin(GlasesObject, GlassesSkin);
                break;
            case SkinType.wap:
                WeopenSkin = s;
                SetupSkin(Weopen, WeopenSkin);
                break;
            default:
                break;
        }
    }

    void SetupSkin(List<GameObject> objects, Skin s)
    {
        foreach (var item in objects)
        {
            if (s==null)
            {
                item.SetActive(false);
            }
            else
            {
                if (item.name == s.name)
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

    void SetupMaterial()
    {
        if (MaterialSkin==null)
        {
            foreach (var item in SkinsObjetcs)
            {
                
                    item.material = DefaultSkin;
                
            }
        }
        else
        {
            int index = UsefulMaterial.FindIndex(d => d.name == MaterialSkin.name);
            foreach (var item in SkinsObjetcs)
            {
                if (index != -1)
                {
                    item.material = UsefulMaterial[index];
                }
            }
        }
      
    }

    void CloseALL()
    {
        GlassesSkin.name = null;
        HatSkin = null;
        MaterialSkin = null;
        WeopenSkin = null;
        PetSkin = null;
        SetupSkin(HatObject, HatSkin);
        SetupSkin(Pet, PetSkin);
        SetupMaterial();
        SetupSkin(GlasesObject, GlassesSkin);
        SetupSkin(Weopen, WeopenSkin);
    }
}
