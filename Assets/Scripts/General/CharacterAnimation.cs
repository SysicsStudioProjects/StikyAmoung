using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GameObject Hands;
    public PlayerEvents playerEvents;
    public AudioSource audio;

    public GameObject DiscObject;

    public GameObject DiscPrfab;
    public GameObject DiscPrefab2;

    public GameObject BusherObject;

    public GameObject BusherPrfab;
    public GameObject BusherPrfab2;

    public GameObject SpearPrefab;
    public PlayerMovement playerMvt;

    public WeopenType weopenType;
    // Start is called before the first frame update
   void OnEnable(){
       EventController.gameWin+=GameWin;
   }
    private void OnDisable()
    {
       EventController.gameWin-=GameWin;
        
    }

    void GameWin(){
        
        if (audio==null)
        {
            return;
        }
        audio.Stop();
        audio.enabled=false;
        controlHands();

    }
    // Update is called once per frame
    

    public void DisableHands()
    {
        if (PlayerEvents.weopenType==WeopenType.none)
        {
            Hands.SetActive(false);
        }
       
    }
    public void ActivateHands()
    {
        Hands.SetActive(true);
    }

    void controlHands()
    {
        if (PlayerEvents.weopenType == WeopenType.none)
        {
            Hands.SetActive(false);
        }
        else
        {
            Hands.SetActive(true);
        }
    }

    Vector3 dir;
   public void Killevent()
    {
        playerMvt.enabled = false;
        Hands.SetActive(true);
        if (playerEvents!=null)
        {
            playerEvents.KillEvent(null);
        }
      
       
        
    }

    public void KnifeKill()
    {
        if (playerMvt != null)
        {
            playerMvt.enabled = false;
        }
        if (playerEvents != null)
        {
            playerEvents.KillEvent(null);
        }
    }

    public void EnableMvt()
    {
        if (playerMvt != null)
        {
            playerMvt.enabled = true;
        }
      
    }

    public void DiscKill()
    {
        string name = "";
        for (int i = 0; i < DiscObject.transform.childCount; i++)
        {
            if (DiscObject.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                name = DiscObject.transform.GetChild(i).gameObject.name;
            }
        }
        GameObject objSpawn=new GameObject();
        switch (name)
        {
            case "Disc1":
                objSpawn = DiscPrfab;
                break;
            case "Disc2":
                objSpawn = DiscPrefab2;
                break;
            case "Spear":
                objSpawn = SpearPrefab;
                break;
            default:
                break;
        }
        Transform Target = playerEvents.target;
        DiscObject.SetActive(false);
        StartCoroutine(ReturnShootbaleObj(DiscObject));
        GameObject obj = Instantiate(objSpawn, DiscObject.transform.position, Quaternion.identity);
        obj.GetComponent<ShootBale>().SetTarget(Target,transform.forward,playerEvents);
       /* if (playerEvents != null)
        {
            playerEvents.KillEvent(null);
        }*/
    }

    public void BucherShoot()
    {
        /* string name = "";
         for (int i = 0; i < BusherObject.transform.childCount; i++)
         {
             if (BusherObject.transform.GetChild(i).gameObject.activeInHierarchy)
             {
                 name = BusherObject.transform.GetChild(i).gameObject.name;
             }
         }
         GameObject objSpawn = new GameObject();
         switch (name)
         {
             case "Butcher_knife1":
                 objSpawn = BusherPrfab;
                 break;
             case "Butcher_knife2":
                 objSpawn = BusherPrfab2;


                 break;
             default:
                 break;
         }
         Transform Target = playerEvents.target;
         BusherObject.SetActive(false);
         StartCoroutine(ReturnShootbaleObj(BusherObject));
         GameObject obj = Instantiate(objSpawn, BusherObject.transform.position, Quaternion.identity);
         obj.GetComponent<ShootBale>().SetTarget(Target, transform.forward, playerEvents);*/
        if (playerMvt != null)
        {
            playerMvt.enabled = false;
        }
        if (playerEvents != null)
        {
            playerEvents.KillEvent(null);
        }
    }

    IEnumerator ReturnShootbaleObj(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(true);
    }
    public void WalkSound()
    {
        if (audio!=null&&audio.enabled==true)
        {
            audio.Play();
        }
        
    }
    private void FixedUpdate()
    {
        if (transform.rotation!=new Quaternion(0,0,0,0))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (weopenType!=PlayerEvents.weopenType)
        {
            controlHands();
            weopenType = PlayerEvents.weopenType;
        }
    }


}
