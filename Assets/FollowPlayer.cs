using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float speed;
    bool canFollow;
    public Animator anim;
    public Vector3 offset;
    private void Start()
    {
        StartCoroutine(followPlayer());
    }
    // Update is called once per frame
    void Update()
    {
        if (!canFollow)
        {
            return;
        }
        Vector3 dir = (target.position + offset) - transform.position;

        if (dir.magnitude <= 0.5f)
        {
            anim.enabled = true;

            speed = 0;

            return;
        }
        if (dir.magnitude>5&&speed<=10)
        {
            speed = 10;
        }
        if (dir.magnitude > 3&& speed<=5)
        {
            speed = 5;
        }
        speed += 0.02f;
        
        float distanceThisFrame = speed * Time.deltaTime;
        anim.enabled = false;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    public void Settarget(Transform t)
    {
        target = t;
    }


    void HitTarget()
    {
        Destroy(gameObject);
    }
    IEnumerator followPlayer()
    {
        yield return new WaitForSeconds(1);
        canFollow = true;
    }
}
