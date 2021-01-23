using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiePatrol : MonoBehaviour
{
    public int indexPoint;
    public int EnnemieId;
    public NavMeshAgent agent;
    [HideInInspector]
    public List<Transform> points;
    [HideInInspector]
    public Transform target;
    public Animator anim;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //Get Patrol points
        EventController.sendPath += SetPoints;
    }

    private void OnDisable()
    {
        EventController.sendPath -= SetPoints;

    }
    private void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target!=null)
        {
            agent.SetDestination(target.position);
            anim.SetFloat("speed", agent.velocity.magnitude);
            if (Vector3.Distance(transform.position, target.position) < 3f)
            {
                ToNextPoint();
            }
        }
        

    }

    void SetPoints(Pathe p)
    {
        //Verife Ennemie Id
        if (p.idEnnemie==EnnemieId)
        {
            points = p.Points;
            ToNextPoint();
        }
    }
    void ToNextPoint()
    {
        if (indexPoint >= points.Count - 1)
        {
            indexPoint = 0;
            target = points[indexPoint];
        }
        else
        {
            indexPoint++;
            target = points[indexPoint];

        }

    }
}
