using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public Transform target;
    public float speed;
    bool canFollow;
    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("PLayer").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFollow)
        {
            return;
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
        Destroy(gameObject);
    }
    IEnumerator FollowPlayer()
    {
        yield return new WaitForSeconds(1);
        canFollow = true;
    }
}
