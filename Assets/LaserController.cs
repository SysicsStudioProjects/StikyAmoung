using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float raduis;
    public bool CanDie;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // GetObjs(transform.position, raduis);
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
            if (EventController.deathWithLaser != null)
            {
                EventController.deathWithLaser();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="PLayer"&&CanDie==true)
        {
            if (EventController.deathWithLaser != null)
            {
                EventController.deathWithLaser();
            }
        }
    }
}
