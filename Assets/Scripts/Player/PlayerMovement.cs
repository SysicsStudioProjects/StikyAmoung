
using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public PlayerEvents playerEvents;
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public Animator anim;
    bool ennemiisTarget;
    public PlayableDirector Brawling;
    
    public float speedRotation;
    public Joystick js;

    public static bool isNotInput;
    public EnnemiePatrol[] allennemies;
    private void OnEnable()
    {
        playerSpeed = 8;
        EventController.canKill += ChangeTarget;
        EventController.sendSettingData += GetSettingData;
        EventController.gameWin += GameWin;
        EventController.gameLoose += GameLoose;
    }

    private void OnDisable()
    {
        EventController.canKill -= ChangeTarget;
        EventController.sendSettingData -= GetSettingData;
        EventController.gameWin -= GameWin;
        EventController.gameLoose -= GameLoose;

        

    }

    private void Start()
    {
        allennemies = GameObject.FindObjectsOfType<EnnemiePatrol>();
        StartCoroutine(CHangeEnnemieState());
    }

    IEnumerator CHangeEnnemieState()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < allennemies.Length; i++)
        {
            if (allennemies[i]!=null)
            {
                EnnemiePatrol e = allennemies[i];
                if (Vector3.Distance(transform.position,e.transform.position)>22)
                {
                    e.GetComponent<Animator>().enabled = false;
                    e.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                    e.GetComponentInChildren<Animator>().enabled = false;
                }
                else
                {
                    e.GetComponent<Animator>().enabled = true;
                    e.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    e.GetComponentInChildren<Animator>().enabled = true;
                }
            }
            
        }
        StartCoroutine(CHangeEnnemieState());
    }

    void Update()
    {
        if (isWin==true||Singleton._instance.state==GameState.win)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
           
            anim.SetFloat("speed", 0);

            return;
        }
        if (switchTarget==null||!switchTarget.gameObject.activeInHierarchy)
        {
            ennemiisTarget = false;
        }

        
        
       
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

       
        Vector3 move = new Vector3(js.Horizontal, 0, js.Vertical);
        if (move==Vector3.zero)
        {
            isNotInput=true;
        }

        anim.SetFloat("speed", Mathf.Abs(move.magnitude * Time.deltaTime * playerSpeed));
        if (move!=Vector3.zero)
        {
            isNotInput = false;
            
            
           
            move.y=0;
            controller.Move(move.normalized * Time.smoothDeltaTime* playerSpeed);
          
        }
        else
        {
            anim.speed = 1;
        }

        if (move != Vector3.zero&& transform.forward.normalized != move.normalized)
        {
            LockOnTarget(move.normalized);
          
        }

      
        
        if (groundedPlayer==false)
        {
            playerVelocity.y = gravityValue * Time.deltaTime;
            controller.Move(playerVelocity);
        }
        
        
        

    }

    void LockOnTarget(Vector3 _target)
    {
        if (ennemiisTarget==false)
        {
            Quaternion startrotation = new Quaternion(0, 0, 0, 0);
            Vector3 root = Vector3.Lerp(transform.forward, _target, speedRotation);
            transform.forward = root;
        }
      
    }

    Transform switchTarget;
        void ChangeTarget(Transform t,float raduis)
    {
        switchTarget = t;
        if (t==null)
        {
            ennemiisTarget = false;

        }
        else if(playerEvents.AutoFocuse == true)
        {
            ennemiisTarget = true;
        }
    }
    
    void GetSettingData(float speed, float ennemydetect, bool autofocuse, bool vibration)
    {
        playerSpeed = speed;

    }
    bool isWin;
    void GameWin()
    {
      
        isWin = true;
      
    }
    void GameLoose()
    {
        this.enabled = false;
    }
    public void PlayBrawling()
    {
        Brawling.Play();
    }
}
