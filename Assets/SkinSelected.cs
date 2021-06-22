using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelected : MonoBehaviour
{
    //hide in inspector;

    public Skin GlassesSkin;
    public Skin HatSkin;
    public Skin MaterialSkin;
    public Skin SpecialSkin;
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
   public static int nbtest;
    public bool isdeath;
    private void OnEnable()
    {
      
        if (Isplaying)
        {
            InitRemove();
            EventController.getSkinstart += SetupSkinStart;
            EventController.gameStart += initStart;
            
        }
        init();
        initStart(false);
       
        // EventController.getSpecialPackage += GetSpecialSkin;
        if (BGSpecialController.skinSetter!=null&&nbtest<=1)
        {
            GetSpecialSkin(BGSpecialController.skinSetter);
            nbtest = 1;
            
        }

        if (isdeath)
        {
            StartSkin = BGSpecialController.skinSetter;
            if (StartSkin != null)
            {
                Skin hat = new Skin();
                Skin skinmaterial = new Skin();
                Skin specialweopen = new Skin();
                char slash = '/';
                string[] n = StartSkin.name.Split(slash);
                hat.name = n[0];
                hat.type = SkinType.hat;
                skinmaterial.name = n[1];
                skinmaterial.type = SkinType.skin;
                specialweopen.name = n[2];
                specialweopen.type = SkinType.wap;
                HatSkin = hat;
                MaterialSkin = skinmaterial;
                SetupMaterial();
                WeopenSkin = specialweopen;
                for (int i = 0; i < Weopen.Count; i++)
                {
                    if (Weopen[i].name == specialweopen.name)
                    {
                        Weopen[i].SetActive(true);
                    }
                    else
                    {
                        Weopen[i].SetActive(false);
                    }
                    Wepon(WeopenSkin);
                }
                for (int i = 0; i < HatObject.Count; i++)
                {
                    if (HatObject[i].name == HatSkin.name)
                    {
                        HatObject[i].SetActive(true);
                    }
                    else
                    {
                        HatObject[i].SetActive(false);
                    }

                }
            }
        }

        EventController.useSkin += SetSkin;
        EventController.removeSkin += RemoveSKin;
    }
    private void OnDisable()
    {
       // EventController.getSpecialPackage -= GetSpecialSkin;

        EventController.useSkin -= SetSkin;
        EventController.removeSkin -= RemoveSKin;
        EventController.getSkinstart -= SetupSkinStart;
        EventController.gameStart -= initStart;

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
        RemoveSKin(HatSkin);
        RemoveSKin(WeopenSkin);
        RemoveSKin(MaterialSkin);
        RemoveSKin(SpecialSkin);

        HatSkin = null;
        WeopenSkin = null;
        MaterialSkin = null;
        SpecialSkin = null;
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
                case SkinType.special:
                    if (item.state==SkinState.useIt)
                    {
                        SpecialSkin = item;
                        Skin hat = new Skin();
                        hat.state = SkinState.useIt;
                        Skin skinmaterial = new Skin();
                        skinmaterial.state = SkinState.useIt;
                        Skin specialweopen = new Skin();
                        specialweopen.state = SkinState.useIt;
                        char slash = '/';
                        string[] n = item.name.Split(slash);
                        hat.name = n[0];
                        hat.type = SkinType.hat;
                        skinmaterial.name = n[1];
                        skinmaterial.type = SkinType.skin;
                        specialweopen.name = n[2];
                        specialweopen.type = SkinType.wap;
                        HatSkin = hat;
                        SetupSkin(HatObject, HatSkin);
                        if (HatSkin.name=="")
                        {
                           
                        }
                        MaterialSkin = skinmaterial;
                        SetupMaterial();
                        if (MaterialSkin.name=="")
                        {
                            
                        }
                        WeopenSkin = specialweopen;
                        Wepon(WeopenSkin);
                        SetupSkin(Weopen, WeopenSkin);
                        if (WeopenSkin.name=="")
                        {
                           
                        }
                        
                       
                    
                       
                    }
                    
                    break;
                default:
                    break;
            }
        }
    }

   void Wepon(Skin s)
    {
        if (s==null)
        {
            PlayerEvents.weopenType = WeopenType.Knife;
            foreach (var item in Singleton._instance.skins.allSkins)
            {
                if (item.name == "Knife1")
                {
                    print("knifeeeeeeeeeeeeeeeeeeeeeee");
                    item.state = SkinState.useIt;
                    WeopenSkin = item;
                    SetupSkin(Weopen, WeopenSkin);
                }


            }
        }
        switch (s.name)
        {
            case "":
                // PlayerEvents.weopenType = WeopenType.none;
                PlayerEvents.weopenType = WeopenType.Knife;
                foreach (var item in Singleton._instance.skins.allSkins)
                {
                    if (item.name== "Knife1")
                    {
                        print("knifeeeeeeeeeeeeeeeeeeeeeee");
                        item.state = SkinState.useIt;
                        WeopenSkin = item;
                        SetupSkin(Weopen, WeopenSkin);
                    }

                   
                }
                break;
            case "Disc1":
                PlayerEvents.weopenType = WeopenType.Disc;
                break;
            case "Disc2":
                PlayerEvents.weopenType = WeopenType.Disc;
                break;
            case "Disc3":
                PlayerEvents.weopenType = WeopenType.Disc;
                break;
            case "Shield_CaptainAmerica":
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
            case "GogoWeopen":
                PlayerEvents.weopenType = WeopenType.gogo;
                break;
            case "IromMan_Weopen":
                PlayerEvents.weopenType = WeopenType.ironman;
                break;
            case "Hammer_Thor":
                PlayerEvents.weopenType = WeopenType.Butcher;
                break;
            default:
                break;
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
     void SetSkin(Skin s)
    {
        /* switch (s.type)
         {
             case SkinType.hat:
                 if (SpecialSkin.name != "")
                 {
                      RemoveSpecialSkin();
                     RemoveSKin(MaterialSkin);
                     RemoveSKin(WeopenSkin);
                     SpecialSkin.name = "";
                 }

                 HatSkin = s;
                 SetupSkin(HatObject, HatSkin);
                 break;
             case SkinType.bette:
                 PetSkin = s;
                 SetupSkin(Pet, PetSkin);
                 break;
             case SkinType.skin:
                 if (SpecialSkin.name != "")
                 {
                     RemoveSpecialSkin();

                     RemoveSKin(WeopenSkin);
                     RemoveSKin(HatSkin);
                     SpecialSkin.name = "";
                 }
                 MaterialSkin = s;
                 SetupMaterial();

                 break;
             case SkinType.glasse:
                 GlassesSkin = s;
                 SetupSkin(GlasesObject,GlassesSkin);
                 break;
             case SkinType.wap:
                 if (SpecialSkin.name != "")
                 {
                     RemoveSpecialSkin();
                     RemoveSKin(MaterialSkin);
                     RemoveSKin(HatSkin);
                     SpecialSkin.name = "";
                 }
                 WeopenSkin = s;
                 SetupSkin(Weopen, WeopenSkin);
                 break;
             case SkinType.special:
                 SpecialSkin = s;
                 Skin hat = new Skin();
                 hat.state = SkinState.useIt;
                 Skin skinmaterial = new Skin();
                 skinmaterial.state = SkinState.useIt;

                 Skin specialweopen = new Skin();
                 specialweopen.state = SkinState.useIt;
                 char slash = '/';
                 string[] n = s.name.Split(slash);
                 hat.name = n[0];
                 hat.type = SkinType.hat;
                 skinmaterial.name = n[1];
                 skinmaterial.type = SkinType.skin;
                 specialweopen.name = n[2];
                 specialweopen.type = SkinType.wap;
                 HatSkin = hat;
                 MaterialSkin = skinmaterial;
                 WeopenSkin = specialweopen;
                 SetupSkin(HatObject, HatSkin);
                 SetupMaterial();
                 SetupSkin(Weopen, WeopenSkin);
                 break;
             default:
                 break;
         }*/
        RemoveSKin(HatSkin);
        RemoveSKin(WeopenSkin);
        RemoveSKin(MaterialSkin);
        RemoveSKin(SpecialSkin);

        HatSkin = null;
        WeopenSkin = null;
        MaterialSkin = null;
        SpecialSkin = null;
       
        foreach (var item in skins.allSkins)
        {
            switch (item.type)
            {
                case SkinType.hat:
                    if (item.state == SkinState.useIt)
                    {
                        HatSkin = item;
                        SetupSkin(HatObject, HatSkin);
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
                    if (item.state == SkinState.useIt)
                    {
                        MaterialSkin = item;
                        SetupMaterial();
                    }
                    if (MaterialSkin==null)
                    {
                        SetupMaterial();
                    }
                    break;
                case SkinType.glasse:
                    if (item.state == SkinState.useIt)
                    {
                        GlassesSkin = item;
                        SetupSkin(GlasesObject, GlassesSkin);
                    }
                    break;
                case SkinType.wap:
                    if (item.state == SkinState.useIt)
                    {
                        WeopenSkin = item;
                        Wepon(WeopenSkin);
                        SetupSkin(Weopen, WeopenSkin);
                    }
                    
                    print(PlayerEvents.weopenType);
                    break;
                case SkinType.special:
                    if (item.state == SkinState.useIt)
                    {
                        SpecialSkin = item;
                        Skin hat = new Skin();
                        hat.state = SkinState.useIt;
                        Skin skinmaterial = new Skin();
                        skinmaterial.state = SkinState.useIt;
                        Skin specialweopen = new Skin();
                        specialweopen.state = SkinState.useIt;
                        char slash = '/';
                        string[] n = item.name.Split(slash);
                        hat.name = n[0];
                        hat.type = SkinType.hat;
                        skinmaterial.name = n[1];
                        skinmaterial.type = SkinType.skin;
                        specialweopen.name = n[2];
                        specialweopen.type = SkinType.wap;
                        HatSkin = hat;
                        SetupSkin(HatObject, HatSkin);
                        if (HatSkin.name == "")
                        {

                        }
                        MaterialSkin = skinmaterial;
                        SetupMaterial();
                        if (MaterialSkin.name == "")
                        {

                        }
                        WeopenSkin = specialweopen;
                        Wepon(WeopenSkin);
                        SetupSkin(Weopen, WeopenSkin);
                        if (WeopenSkin.name == "")
                        {

                        }




                    }

                    break;
                default:
                    break;
            }
        }
        // GlassesSkin = s;

    }

    void RemoveSpecialSkin()
    {
       
      //  RemoveSKin(HatSkin);
       // RemoveSKin(WeopenSkin);
      //  RemoveSKin(MaterialSkin);
        SpecialSkin = null;
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
        if (s==null)
        {
            return;
        }
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
            case SkinType.special:
                SpecialSkin = null;
                WeopenSkin = null;
                PlayerEvents.weopenType = WeopenType.none;
                SetupRemove(Weopen);
                MaterialSkin = null;
                foreach (var item in SkinsObjetcs)
                {


                    item.material = DefaultSkin;

                }
                HatSkin = null;
                SetupRemove(HatObject);
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
        if (HatSkin!=null)
        {
            if (HatSkin.state == SkinState.buyIt)
            {
                HatSkin = null;
                SetupRemove(HatObject);
            }
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
        if (WeopenSkin!=null)
        {
            if (WeopenSkin.state == SkinState.buyIt)
            {
                WeopenSkin = null;
                SetupRemove(Weopen);
            }
        }

        if (MaterialSkin!=null)
        {
            if (MaterialSkin.state == SkinState.buyIt)
            {
                MaterialSkin = null;
                foreach (var item in SkinsObjetcs)
                {


                    item.material = DefaultSkin;

                }
            }
        }
        else
        {
            foreach (var item in SkinsObjetcs)
            {


                item.material = DefaultSkin;

            }
        }
        if (SpecialSkin!=null)
        {
            if (SpecialSkin.state == SkinState.buyIt)
            {
                RemoveSpecialSkin();
            }
        }
        
    }
    Skin StartSkin;
    void SetupSkinStart(Skin s)
    {
        StartSkin = s;
        initStart(false);


    }
    void initStart(bool b)
    {
        print("we are here and we need to change plz");
        if (StartSkin==null)
        {
            return;
        }
        switch (StartSkin.type)
        {
            case SkinType.hat:
                for (int i = 0; i < HatObject.Count; i++)
                {
                    if (HatObject[i].name==StartSkin.name)
                    {
                        HatObject[i].SetActive(true);
                    }
                    else
                    {
                        HatObject[i].SetActive(false);
                    }
                  
                }

                break;
           
            case SkinType.glasse:
                for (int i = 0; i < GlasesObject.Count; i++)
                {
                    if (GlasesObject[i].name == StartSkin.name)
                    {
                        GlasesObject[i].SetActive(true);
                    }
                    else
                    {
                        GlasesObject[i].SetActive(false);
                    }
                  
                }
                break;
            case SkinType.wap:
                for (int i = 0; i < Weopen.Count; i++)
                {
                    if (Weopen[i].name == StartSkin.name)
                    {
                        Weopen[i].SetActive(true);
                    }
                    else
                    {
                        Weopen[i].SetActive(false);
                    }
                    Wepon(StartSkin);
                }
                break;

            
            case SkinType.special:
                if (StartSkin.type!=SkinType.special)
                {
                    RemoveSpecialSkin();
                    return;
                }
                Skin hat = new Skin();
                Skin skinmaterial = new Skin();
                Skin specialweopen = new Skin();
                char slash = '/';
                string[] n = StartSkin.name.Split(slash);
                hat.name = n[0];
                hat.type = SkinType.hat;
                skinmaterial.name = n[1];
                skinmaterial.type = SkinType.skin;
                specialweopen.name = n[2];
                specialweopen.type = SkinType.wap;
                HatSkin = hat;
                MaterialSkin = skinmaterial;
                SetupMaterial();
                WeopenSkin = specialweopen;
                for (int i = 0; i < Weopen.Count; i++)
                {
                    if (Weopen[i].name == specialweopen.name)
                    {
                        Weopen[i].SetActive(true);
                    }
                    else
                    {
                        Weopen[i].SetActive(false);
                    }
                    Wepon(WeopenSkin);
                }
                for (int i = 0; i < HatObject.Count; i++)
                {
                    if (HatObject[i].name == HatSkin.name)
                    {
                        HatObject[i].SetActive(true);
                    }
                    else
                    {
                        HatObject[i].SetActive(false);
                    }

                }
                break;
            default:
                break;
        }
    }

     void GetSpecialSkin(Skin s)
    {

        //StartSkin = s;
        StartCoroutine(ActivateMagic(s));
    }

    public GameObject Magic;

    IEnumerator ActivateMagic(Skin s)
    {
        yield return new WaitForSeconds(0.2f);
        Magic.SetActive(true);
        StartSkin = s;
        yield return new WaitForSeconds(0.3f);
      
        if (StartSkin != null)
        {
            Skin hat = new Skin();
            Skin skinmaterial = new Skin();
            Skin specialweopen = new Skin();
            char slash = '/';
            string[] n = StartSkin.name.Split(slash);
            hat.name = n[0];
            hat.type = SkinType.hat;
            skinmaterial.name = n[1];
            skinmaterial.type = SkinType.skin;
            specialweopen.name = n[2];
            specialweopen.type = SkinType.wap;
            HatSkin = hat;
            MaterialSkin = skinmaterial;
            SetupMaterial();
            WeopenSkin = specialweopen;
            for (int i = 0; i < Weopen.Count; i++)
            {
                if (Weopen[i].name == specialweopen.name)
                {
                    Weopen[i].SetActive(true);
                }
                else
                {
                    Weopen[i].SetActive(false);
                }
                Wepon(WeopenSkin);
            }
            for (int i = 0; i < HatObject.Count; i++)
            {
                if (HatObject[i].name == HatSkin.name)
                {
                    HatObject[i].SetActive(true);
                }
                else
                {
                    HatObject[i].SetActive(false);
                }

            }
        }
        yield return new WaitForSeconds(5);
        Magic.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="pickup")
        {
            Skin pickupSkin = other.GetComponent<Pickup>().thisskin;
            SetupSkinStart(pickupSkin);
            other.transform.root.gameObject.SetActive(false);
            //Destroy(other.transform.root.gameObject);
        }
    }
}
