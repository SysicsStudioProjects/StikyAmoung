
using UnityEngine;
using UnityEngine.AI;
public class EnnemeieBonuse : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    public Animator anim;

    public SkinnedMeshRenderer BodyRendered;
    public SkinnedMeshRenderer HandLeftRendere;
    public SkinnedMeshRenderer HandRightRendere;
    public Color MaterialColor;
    
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        SetupMaterial();
        wanderTimer = Random.Range(1, 5);
     //   agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        EventController.gameStart+=GameStart;
    }

    void SetupMaterial()
    {
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", MaterialColor);
        BodyRendered.materials[0].color = MaterialColor;
        HandLeftRendere.SetPropertyBlock(block);
        HandRightRendere.SetPropertyBlock(block);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (BodyRendered.isVisible)
        {
            anim.enabled = true;
            
        }
        else{
             anim.enabled = false;
        }
        if(isgameStart){
        timer += Time.deltaTime;
          
        if (timer >= wanderTimer)
        {
                timer = 0;
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
               

            }

            anim.SetFloat("speed", agent.velocity.magnitude);

        }
        
    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
      public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
      {
          Vector3 randDirection = Random.insideUnitSphere * dist;

          randDirection += origin;

          NavMeshHit navHit;

          NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

          return navHit.position;
      }

    public void DetectPlayer(Transform t)
    {
       

    }
    private void OnDisable()
    {
        EventController.gameStart-=GameStart;

        if (isgameStart){
        if (EventController.ennemieDown != null)
        {
            EventController.ennemieDown(null);
        }
        }

    }
    private void OnDestroy()
    {
       
        
    }
     bool isgameStart;
    void GameStart(bool b){
        isgameStart=b;
    }
}
