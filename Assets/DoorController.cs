﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool HasAcard;
    public CardType cardType;
    public Animator anim;
    public float raduis;
    public Collider collider;

    int IndexDoor;
  
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventController.hasACard += SetCard;
        StartCoroutine(FindObj());
    }

    private void OnDisable()
    {
        EventController.hasACard -= SetCard;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
       
       
    }
    void GetObjs(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Transform> objs = new List<Transform>();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.GetComponent<PlayerEvents>())
            {
                objs.Add(hitColliders[i].transform);
            }
        }

        if (objs.Count > 0)
        {
            collider.enabled = false;
            if (anim.GetBool("Isopen")==false)
            {
                anim.SetBool("Isopen", true);
            }
            
        }
        else
        {
            collider.enabled = true;
            if (anim.GetBool("Isopen") == true)
            {
                anim.SetBool("Isopen", false);
            }
        }
    }
    void GetObjsError(Vector3 center, float radius)
    {
       
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Transform> objs = new List<Transform>();
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.GetComponent<PlayerEvents>())
            {
                objs.Add(hitColliders[i].transform);
            }
        }

        if (objs.Count > 0)
        {
            if(IndexDoor==0){
                IndexDoor=1;
            //You need Card 
            if (EventController.cardRequired!=null)
            {
                EventController.cardRequired(true);
            }

            }
            
        }
        else
        {
            if (IndexDoor==1){
                if (EventController.cardRequired != null)
            {
                EventController.cardRequired(false);
            }
            IndexDoor=0;
            }
            
        }
        
    }

    void SetCard(CardType _cardType)
    {
        if (cardType==_cardType)
        {
            HasAcard = true;
        }
    }

    IEnumerator FindObj(){

         if (HasAcard)
        {
            GetObjs(transform.position,raduis);
            yield return new WaitForSeconds(0.5f);
        }
  
         else
        {
            GetObjsError(transform.position, raduis);
            yield return new WaitForSeconds(0.5f);
        }

      StartCoroutine(FindObj());

    }
}

public enum CardType { red,blue,white }
