using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiePatrol : MonoBehaviour
{
    Renderer m_Renderer;
    public int indexPoint;
    public int EnnemieId;
   // public NavMeshAgent agent;
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
    public Renderer FiedlRender;
    public GameObject PortalDown;

    public Color MaterialColor;
    public bool IsTask;

    bool isStopping;
    public float speed;
    public EnemyDiffuclty enemyDiffuclty;
    // Start is called before the first frame update
    private void OnEnable()
    {
       
        SetupMaterial();
       
        EventController.sendPath += SetPoints;
        EventController.canKill += ChangeTarget;
        EventController.sendSettingData += GetSettingData;
        if (EnnemieId < 0&&pathe.Points.Count>0&&IsGameStart)
        {
            ToNextPoint();
        }
        EventController.setPlayer += GetPlayer;
        EventController.gameStart += GameStart;

        switch (enemyDiffuclty)
        {
            case EnemyDiffuclty.None:
                timeTodetect = 0.5f;
                break;
            case EnemyDiffuclty.Basic:
                view.enabled = false;
                break;
            case EnemyDiffuclty.Meduim:
                timeTodetect = 0.3f;
                StartCoroutine(SwitchViewState());
                break;
            case EnemyDiffuclty.Hard:
                timeTodetect = 0.3f;
                break;
            case EnemyDiffuclty.VeryHard:
                timeTodetect = 0.1f;
                break;
            default:
                break;
        }

    }

    IEnumerator SwitchViewState()
    {
        view.enabled = false;
        Field.SetActive(false);
        yield return new WaitForSeconds(4);
        Field.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        view.enabled = true;
        while (startDetect)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);

        StartCoroutine(SwitchViewState());
    }

    private void OnDisable()
    {
        StopCoroutine("WaitingDiePlayer");
        StopCoroutine(VerifPointDistance());
        startDetect = false;
        if (EventController.canKill!=null)
        {
            EventController.canKill(null, 0);
        }
        EventController.sendPath -= SetPoints;
        EventController.canKill -= ChangeTarget;
        EventController.sendSettingData -= GetSettingData;
       
        if (IsGameStart){
        if (EventController.ennemieDown != null)
        {
            EventController.ennemieDown(this);
             pathe.isAlive = false;
        }
        }
        EventController.setPlayer -= GetPlayer;
        EventController.gameStart -= GameStart;

    }
    void SetupMaterial()
    {
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", MaterialColor);
        if (BodyRendered.gameObject.activeInHierarchy)
        {
            BodyRendered.materials[0].color = MaterialColor;
            HandLeftRendere.SetPropertyBlock(block);
            HandRightRendere.SetPropertyBlock(block);
        }
      
    }
    private void Start()
    {

       // player = GameObject.FindGameObjectWithTag("PLayer").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (IsGameStart==false)
        {
            return;
        }
        if (BodyRendered.gameObject.activeInHierarchy)
        {
            if (BodyRendered.isVisible)
            {
                anim.enabled = true;
                //view.enabled = true;
            }
            else
            {
                anim.enabled = false;
                //view.enabled = false;

            }
        }
        if (FiedlRender.isVisible)
        {
            view.canFind = true;
        }
        else
        {
            view.canFind = false;
        }
       
        if (_DetectPlayer!=null)
        {
            //  agent.enabled = false;
            isStopping = true;
            return;

        }
        else
        {
            startDetect = false;
            // agent.enabled = true;
            isStopping = false;
        }
        if (focuseToPlayer&&targetPlayer==player.transform)
        {
            print("we are here");
            //agent.enabled = false;
            isStopping = true;
            Vector3 dir = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f).eulerAngles;
            // LeanTween.rotate(transform, new Vector3(0,dir.y,0), 0.3f);
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            
        }
        if (target!=null&&isStopping==false)
        {
            // agent.angularSpeed = 120;

            //  agent.speed = 3.5f;
            // agent.SetDestination(target.position);
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            //transform.LookAt(target.position);
            LockOnTarget();
            anim.SetFloat("speed", 1);
            
        }
      
        

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
        if (IsTask)
        {
            if (startEneum==true)
            {
                return;
            }
            StartCoroutine(WaitingTask());
        }
        else
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

    }

    IEnumerator VerifPointDistance()
    {
        yield return new WaitForSeconds(0.2f);
        if (target==null)
        {
            StartCoroutine(VerifPointDistance());
        
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) < 1f && target != player)
            {
                ToNextPoint();
            }
            StartCoroutine(VerifPointDistance());
        }
          

       
    }
    bool startEneum;
    IEnumerator WaitingTask()
    {
        startEneum = true;
        target = null;
        // agent.enabled = false;
        isStopping = true;
        anim.SetFloat("speed", 0);
        yield return new WaitForSeconds(3);
        //agent.enabled = true;
        isStopping = false;
       
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
        startEneum = false;
    }

    bool startFocuseToPlayer;
    void ChangeTarget(Transform t,float f)
    {

        if (t==transform)
        {
            targetPlayer = player;
            // focuseToPlayer = true;
            if (BodyRendered.gameObject.activeInHierarchy)
            {
                ParticulePortal.SetActive(true);
            }
           //Here will make focuseToPlayer true with the timeline

        }
        else
        {
            targetPlayer = null;

            //  agent.enabled = true;
            isStopping = false;
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
        if (_DetectPlayer==player&&startDetect==true && Field.activeInHierarchy)
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
    void GameStart(bool b)
    {
        StartCoroutine(VerifPointDistance());
        IsGameStart = b;
        Field.SetActive(true);
        if (BodyRendered.gameObject.activeInHierarchy)
        {
            PortalDown.SetActive(true);
        }
        
        if (EnnemieId < 0 && pathe.Points.Count > 0)
        {
            ToNextPoint();
        }

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}

public enum EnemyDiffuclty { None, Basic, Meduim, Hard, VeryHard}


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
