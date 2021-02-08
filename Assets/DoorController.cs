using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool HasAcard;
    public CardType cardType;
    public Animator anim;
    public float raduis;
    public Collider collider;
  
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventController.hasACard += SetCard;
    }

    private void OnDisable()
    {
        EventController.hasACard -= SetCard;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasAcard)
        {
            GetObjs(transform.position,raduis);
        }
        else
        {
            GetObjsError(transform.position, raduis);
        }
       
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
            
            //You need Card 
            if (EventController.cardRequired!=null)
            {
                EventController.cardRequired(true);
            }
        }
        else
        {
            if (EventController.cardRequired != null)
            {
                EventController.cardRequired(false);
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
}

public enum CardType { red,blue,white }
