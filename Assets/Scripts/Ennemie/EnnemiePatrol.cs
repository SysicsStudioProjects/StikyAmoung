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
    Transform player;
    public GameObject ParticulePortal;
    public GameObject DeathPlayer;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //Get Patrol points
        EventController.sendPath += SetPoints;
        EventController.canKill += ChangeTarget;
       
    }

    private void OnDisable()
    {
        EventController.sendPath -= SetPoints;
        EventController.canKill -= ChangeTarget;

    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLayer").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (target!=null&&EnnemieId>=0)
        {
            agent.angularSpeed = 120;
           ;
            agent.speed = 3.5f;
            agent.SetDestination(target.position);
        
            if (Vector3.Distance(transform.position, target.position) < 3f&&target!=player)
            {
                ToNextPoint();
            }
        }
        if(target==player)
        {
            agent.SetDestination(target.position);
        }
        if (startFocuseToPlayer)
        {
            agent.angularSpeed = 0;
            agent.speed = 1;
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance<1)
            {
                Vector3 dir = player.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.5f).eulerAngles;
                // LeanTween.rotate(transform, new Vector3(0,dir.y,0), 0.3f);
                transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            }
            else
            {
                Vector3 dir = player.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.01f).eulerAngles;
                // LeanTween.rotate(transform, new Vector3(0,dir.y,0), 0.3f);
                transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            }
          
        }
        else if (EnnemieId<0)
        {
            agent.angularSpeed = 120;
            target = null;
            agent.speed = 3.5f;
        }
        anim.SetFloat("speed", agent.velocity.magnitude);

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

    bool startFocuseToPlayer;
    void ChangeTarget(Transform t)
    {

        if (t==transform)
        {
            target = player;
            startFocuseToPlayer = true;

            ParticulePortal.SetActive(true);


        }
        else
        {
            if (EnnemieId!=-1)
            {

                if (target==player)
                {
                    target = points[indexPoint];
                }
            }
           
           
            startFocuseToPlayer = false;
            ParticulePortal.SetActive(false);

        }

    }

   


}
