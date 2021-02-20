using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBale : MonoBehaviour
{
    Transform target;
    public float speed;
    //public Rigidbody rb;
    public Animator anim;
    //public Collider collider;
    private void FixedUpdate()
    {
        /*if (target==null)
        {
            rb.velocity=(parent *Time.smoothDeltaTime*800);
        }*/
        if (target==null||!target.gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
            speed += 0.02f;
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= 0.5f)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
        
        
    }

    void HitTarget()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame

    Vector3 parent;
    PlayerEvents playerEvents;
    public void SetTarget(Transform t,Vector3 _parent,PlayerEvents _playerEvents)
    {
        target = t;
        parent = _parent;
        playerEvents = _playerEvents;
    }

    private void OnCollisionEnter(Collision collision)
    {

      
        
        if (collision.transform.tag=="Ennemie")
        {
            
            playerEvents.KillEvent(collision.transform);
            Destroy(gameObject);
        }
        else if (collision.transform.tag!="PLayer")
        
        {
            // Destroy(gameObject);

          
            Destroy(gameObject);

        }
        
    }
}
