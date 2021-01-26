using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GameObject Hands;
    public PlayerEvents playerEvents;
    public AudioSource audio;
    
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
        Hands.SetActive(false);
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
            playerEvents.KillEvent();
        }
      
       
        
    }
    public void WalkSound()
    {
        if (audio!=null)
        {
            audio.Play();
        }
        
    }


}
