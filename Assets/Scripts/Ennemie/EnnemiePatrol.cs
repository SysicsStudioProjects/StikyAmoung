using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiePatrol : MonoBehaviour
{
    public int indexPoint;
    public int EnnemieId;
    public NavMeshAgent agent;
    //[HideInInspector]
   // public List<Transform> points;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Transform targetPlayer;
    public Animator anim;
    Transform player;
    public GameObject ParticulePortal;
    public GameObject DeathPlayer;
    public bool focuseToPlayer;
    public Pathe pathe;
    public float timeTodetect;

    public Transform _DetectPlayer;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("detect"))
        {
            timeTodetect = PlayerPrefs.GetFloat("detect");
        }
        //Get Patrol points
        EventController.sendPath += SetPoints;
        EventController.canKill += ChangeTarget;
        EventController.sendSettingData += GetSettingData;
        if (EnnemieId < 0&&pathe.Points.Count>0)
        {
            ToNextPoint();
        }
    }

    private void OnDisable()
    {
        EventController.sendPath -= SetPoints;
        EventController.canKill -= ChangeTarget;
        EventController.sendSettingData -= GetSettingData;


    }
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("PLayer").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (focuseToPlayer&&targetPlayer==player.transform)
        {
            
            agent.enabled = false;
            Vector3 dir = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f).eulerAngles;
            // LeanTween.rotate(transform, new Vector3(0,dir.y,0), 0.3f);
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            
        }
        if (target!=null&&agent.enabled==true)
        {
            agent.angularSpeed = 120;
           
            agent.speed = 3.5f;
            agent.SetDestination(target.position);
        
            if (Vector3.Distance(transform.position, target.position) < 1f&&target!=player)
            {
                ToNextPoint();
            }
        }
      
        anim.SetFloat("speed", agent.velocity.magnitude);

    }

    void SetPoints(Pathe p)
    {
        //Verife Ennemie Id
        if (p.idEnnemie==EnnemieId)
        {
            pathe = p;
            ToNextPoint();
            
        }
    }
    void ToNextPoint()
    {
        if (indexPoint >= pathe.Points.Count - 1)
        {
            indexPoint = 0;
            target = pathe.Points[indexPoint];
        }
        else
        {
            indexPoint++;
            target = pathe.Points[indexPoint];

        }

    }

    bool startFocuseToPlayer;
    void ChangeTarget(Transform t)
    {

        if (t==transform)
        {
            targetPlayer = player;
            //startFocuseToPlayer = true;

            ParticulePortal.SetActive(true);


        }
        else
        {
            targetPlayer = null;
            agent.enabled = true;
           /* if (EnnemieId!=-1)
            {

                if (target==player)
                {
                    target = points[indexPoint];
                }
            }*/


            // startFocuseToPlayer = false;
            ParticulePortal.SetActive(false);

        }

    }


    private void OnDestroy()
    {
        pathe.isAlive = false;
        if (EventController.ennemieDown != null)
        {
            EventController.ennemieDown(this);
        }
        
    }

    bool startDetect;
    public void DetectPlayer(Transform t)
    {
        _DetectPlayer = t;
        if (t==null)
        {
            StopCoroutine("WaitingDiePlayer");
            startDetect = false;
        }
        if (startDetect==false&&t!=null)
        {
           StartCoroutine( WaitingDiePlayer(timeTodetect));
        }
     
    }

    IEnumerator WaitingDiePlayer(float time)
    {
        print("start die");
        startDetect = true;
        yield return new WaitForSeconds(time);
        if (_DetectPlayer==player&&startDetect==true)
        {
            print("hummm i kill you");
            if (EventController.gameLoose!=null)
            {
                EventController.gameLoose();
            }
        }
        startDetect = false;
    }

    void GetSettingData(float speed, float ennemydetect, bool autofocuse, bool vibration)
    {
        timeTodetect = ennemydetect;

    }
}


/*  if(target==player)
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
        }*/
