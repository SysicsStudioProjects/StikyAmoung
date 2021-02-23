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
    public Material DefaultSkin;
    public Skins skins;

    public List<GameObject> Weopen;
    public Skin WeopenSkin;

    public List<GameObject> Pet;
    public Skin PetSkin;

    public bool Isplaying;
    public List<GameObject> PetPrefabs;

    public Transform PetPos;
    private void OnEnable()
    {
        if (Isplaying)
        {
            InitRemove();
        }
        init();
       
        EventController.useSkin += SetSkin;
        EventController.removeSkin += RemoveSKin;
    }
    private void OnDisable()
    {
        EventController.useSkin -= SetSkin;
        EventController.removeSkin -= RemoveSKin;

        if (PetFollow!=null)
        {
            Destroy(PetFollow);
        }

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
                        if (Isplaying)
                        {
                            SetupSkinPrefab(PetPrefabs, PetSkin);
                        }
                        else
                        {
                            SetupSkin(Pet, PetSkin);

                        }
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
                        Wepon(WeopenSkin);
                        SetupSkin(Weopen, WeopenSkin);
                    }
                    if (item.name=="")
                    {
                        PlayerEvents.weopenType = WeopenType.none;
                    }
                    print(PlayerEvents.weopenType);
                    break;
                default:
                    break;
            }
        }
    }

   void Wepon(Skin s)
    {
        switch (s.name)
        {
            case "Disc1":
                PlayerEvents.weopenType = WeopenType.Disc;
                break;
            case "Disc2":
                PlayerEvents.weopenType = WeopenType.Disc;
                break;
            case "Knife1":
                PlayerEvents.weopenType = WeopenType.Knife;
                break;
            case "Knife2":
                PlayerEvents.weopenType = WeopenType.Knife;
                break;
            case "Butcher_knife1":
                PlayerEvents.weopenType = WeopenType.Butcher;
                break;
            case "Butcher_knife2":
                PlayerEvents.weopenType = WeopenType.Butcher;
                break;
            case "Spear":
                PlayerEvents.weopenType = WeopenType.Disc;
                break;
            case "Knife4":
                PlayerEvents.weopenType = WeopenType.Knife;
                break;
            case "Knife3":
                PlayerEvents.weopenType = WeopenType.Knife;
                break;
            case "Axe_weapon":
                PlayerEvents.weopenType = WeopenType.Butcher;
                break;
            default:
                break;
        }
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

    GameObject PetFollow;
    void SetupSkinPrefab(List<GameObject> objects, Skin s)
    {
        foreach (var item in objects)
        {
            if (item.name == s.name)
            {
                GameObject obj = Instantiate(item, PetPos.position , Quaternion.identity);
                obj.SetActive(true);
                PetFollow = obj;
                obj.GetComponent<FollowPlayer>().Settarget(transform);
            }
            else
            {
                item.SetActive(false);
            }
        }
       
    }
    
    void RemoveSKin(Skin s)
    {
        switch (s.type)
        {
            case SkinType.hat:
                HatSkin = null;
                SetupRemove(HatObject);
                break;

            case SkinType.bette:
                PetSkin = null;
                SetupRemove(Pet);


                break;
            case SkinType.skin:
                MaterialSkin = null;
                foreach (var item in SkinsObjetcs)
                {
                    
                    
                        item.material = DefaultSkin;
                    
                }
                break;
            case SkinType.glasse:
                GlassesSkin = null;
                SetupRemove(GlasesObject);
                break;
            case SkinType.wap:
                WeopenSkin = null;
                PlayerEvents.weopenType = WeopenType.none;
                SetupRemove(Weopen);
                break;
            default:
                break;
        }
      
    }

    void SetupRemove(List<GameObject> a)
    {
        foreach (var item in a)
        {
            item.SetActive(false);
        }
    }
    void InitRemove()
    {
        if (GlassesSkin.state==SkinState.buyIt)
        {
            GlassesSkin = null;
            SetupRemove(GlasesObject);
        }
        if (HatSkin.state== SkinState.buyIt)
        {
            HatSkin = null;
            SetupRemove(HatObject);
        }
        if (PetSkin!=null)
        {
            if (PetSkin.state == SkinState.buyIt)
            {
                PetSkin = null;
                Destroy(PetFollow);
            }
        }
        else
        {
            Destroy(PetFollow);
        }
        
        if (WeopenSkin.state==SkinState.buyIt)
        {
            WeopenSkin = null;
            SetupRemove(Weopen);
        }
        if (MaterialSkin.state==SkinState.buyIt)
        {
            MaterialSkin = null;
            foreach (var item in SkinsObjetcs)
            {


                item.material = DefaultSkin;

            }
        }
    }
}
