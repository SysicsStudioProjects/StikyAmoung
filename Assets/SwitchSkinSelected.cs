using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkinSelected : MonoBehaviour
{
    public Skin GlassesSkin;
    public Skin HatSkin;
    public Skin MaterialSkin;
    public Skin SpecielSkin;
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

    public List<SpecialPackage> specialPackages;
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
            case SkinType.special:
                SpecielSkin = s;
                Skin hat = new Skin();
                Skin skinmaterial = new Skin();
                Skin specialweopen = new Skin();
                char slash = '/';
                string[] n = s.name.Split(slash);
                hat.name = n[0];
                hat.type = SkinType.hat;
                skinmaterial.name = n[1];
                skinmaterial.type = SkinType.skin;
                specialweopen.name = n[2];
                specialweopen.type = SkinType.wap;
               // HatSkin = hat;
                SetupSkin(HatObject, hat);
               MaterialSkin = skinmaterial;
                SetupMaterial();
               // WeopenSkin = specialweopen;
                SetupSkin(Weopen, specialweopen);
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
        SpecielSkin = null;
        SetupSkin(HatObject, HatSkin);
        SetupSkin(Pet, PetSkin);
        SetupMaterial();
        SetupSkin(GlasesObject, GlassesSkin);
        SetupSkin(Weopen, WeopenSkin);
    }
}


[System.Serializable]
public class SpecialPackage
{
    public string SkinName;
    public Material skinMaterial;
    public GameObject hats;
    public GameObject weopen;
    public GameObject[] otherobjs;
    
}