using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiePatrol : MonoBehaviour
{
    Renderer m_Renderer;
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
    
    public bool focuseToPlayer;
    public Pathe pathe;
    public float timeTodetect;

    public Transform _DetectPlayer;

    public SkinnedMeshRenderer BodyRendered;
    public SkinnedMeshRenderer HandLeftRendere;
    public SkinnedMeshRenderer HandRightRendere;
    public FieldOfView view;
    [HideInInspector]
    public bool IsGameStart;

    public GameObject Field;
    public GameObject PortalDown;

    public Color MaterialColor;
    // Start is called before the first frame update
    private void OnEnable()
    {
       
        SetupMaterial();
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
        EventController.setPlayer += GetPlayer;
        EventController.gameStart += GameStart;

    }

    private void OnDisable()
    {
        StopCoroutine("WaitingDiePlayer");
        startDetect = false;
        EventController.sendPath -= SetPoints;
        EventController.canKill -= ChangeTarget;
        EventController.sendSettingData -= GetSettingData;
        pathe.isAlive = false;
        if (EventController.ennemieDown != null)
        {
            EventController.ennemieDown(this);
        }
        EventController.setPlayer -= GetPlayer;
        EventController.gameStart -= GameStart;

    }
    void SetupMaterial()
    {
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", MaterialColor);
        BodyRendered.materials[0].color = MaterialColor;
        HandLeftRendere.SetPropertyBlock(block);
        HandRightRendere.SetPropertyBlock(block);
    }
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("PLayer").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsGameStart==false)
        {
            return;
        }
        if (BodyRendered.isVisible)
        {
            anim.enabled = true;
            view.enabled = true;
        }
        else
        {
            anim.enabled = false;
            view.enabled = false;

        }
        if (_DetectPlayer!=null)
        {
            agent.enabled = false;
            return;

        }
        else
        {
            startDetect = false;
            agent.enabled = true;
        }
        if (focuseToPlayer&&targetPlayer==player.transform)
        {
            print("we are here");
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
    void ChangeTarget(Transform t,float f)
    {

        if (t==transform)
        {
            targetPlayer = player;
           // focuseToPlayer = true;

            ParticulePortal.SetActive(true);
           //Here will make focuseToPlayer true with the timeline

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


            focuseToPlayer = false;
            ParticulePortal.SetActive(false);

        }

    }


   IEnumerator RotateToplayer()
    {
        yield return new WaitForSeconds(2);
        if (targetPlayer==player)
        {
            focuseToPlayer = true;
        }
        else
        {
            focuseToPlayer = false;
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
       else if (startDetect==false&&t!=null)
        {
          
           StartCoroutine( WaitingDiePlayer(timeTodetect));
            startDetect = true;
        }
        
     
    }

    IEnumerator WaitingDiePlayer(float time)
    {
        print("start die");
        
        yield return new WaitForSeconds(time);
        if (_DetectPlayer==player&&startDetect==true)
        {

            ValidateDetected();
        }
        
    }
    void ValidateDetected()
    {
        if (_DetectPlayer==null||startDetect==false)
        {
            return;
        }
        anim.SetTrigger("angry");
        if (EventController.gameLoose != null)
        {
            EventController.gameLoose();
        }
        startDetect = false;
    }

    void GetSettingData(float speed, float ennemydetect, bool autofocuse, bool vibration)
    {
        timeTodetect = ennemydetect;

    }

    void GetPlayer(Transform p)
    {
        player = p;
    }
    void GameStart()
    {
        IsGameStart = true;
        Field.SetActive(true);
        PortalDown.SetActive(true);
        if (EnnemieId < 0 && pathe.Points.Count > 0)
        {
            ToNextPoint();
        }
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
