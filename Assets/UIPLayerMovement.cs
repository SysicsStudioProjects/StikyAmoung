using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPLayerMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    public List<Transform> points;

    bool canMove;

    private void OnEnable()
    {
        
        wavepointIndex = 0;
        target = points[0];
        transform.position = target.position;
        //transform.rotation = target.rotation;
        canMove = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove==false)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized , Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        //transform.LookAt(target);
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= points.Count - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = points[wavepointIndex];
    }

    void EndPath()
    {
        canMove = false;
    }

}
