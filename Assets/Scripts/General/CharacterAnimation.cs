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

    public GameObject BusherObject;

    public GameObject BusherPrfab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    Vector3 dir;
   public void Killevent()
    {
        Hands.SetActive(true);
        if (playerEvents!=null)
        {
            playerEvents.KillEvent(null);
        }
      
       
        
    }

    public void KnifeKill()
    {
        if (playerEvents != null)
        {
            playerEvents.KillEvent(null);
        }
    }

    public void DiscKill()
    {
        Transform Target = playerEvents.target;
        DiscObject.SetActive(false);
        StartCoroutine(ReturnShootbaleObj(DiscObject));
        GameObject obj = Instantiate(DiscPrfab, DiscObject.transform.position, Quaternion.identity);
        obj.GetComponent<ShootBale>().SetTarget(Target,transform.forward,playerEvents);
        if (playerEvents != null)
        {
            playerEvents.KillEvent(null);
        }
    }

    public void BucherShoot()
    {
        Transform Target = playerEvents.target;
        BusherObject.SetActive(false);
        StartCoroutine(ReturnShootbaleObj(BusherObject));
        GameObject obj = Instantiate(BusherPrfab, BusherObject.transform.position, Quaternion.identity);
        obj.GetComponent<ShootBale>().SetTarget(Target, transform.forward, playerEvents);
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
        if (audio!=null)
        {
            audio.Play();
        }
        
    }


}
