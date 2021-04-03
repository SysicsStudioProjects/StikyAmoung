using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovementController : MonoBehaviour
{
    public bool moveK;
    public bool MoveJ;
    //public Joystick js;
    //send x joystic pos
   static float x;
    //send y joystic pos
  static  float y;
    public Button KillButton;
    bool stopMvt;

    public GameObject TeleportButton;

    private void OnEnable()
    {
       // StartCoroutine(StartCaroutineKillEvent(0.5f));
       // EventController.canKill += ChangeButtonBehavior;
        //EventController.gameWin += GameWin;
     /*   if (PlayerEvents.weopenType==WeopenType.Disc)
        {
            KillButton.interactable = true;
        }*/

        //disableDiscButton = true;
        EventController.teleportCollision += EnableTeleportButton;
    }

    private void OnDisable()
    {
       // EventController.canKill -= ChangeButtonBehavior;
       // EventController.gameWin -= GameWin;
        EventController.teleportCollision -= EnableTeleportButton;
       // x = 0;
       // y = 0;
        stopMvt = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
   /* void Update()
    {
       // Application.targetFrameRate = 30;
        if (stopMvt == true)
        {

            if (MoveJ)
            {
               
                MoveJoystic(js.Horizontal, js.Vertical);
            }
            if (moveK)
            {
                MoveKeys(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }
    }*/

    public  void MoveJoystic(float _x,float _y)
    {
        x = _x;
        y = _y; 
    }

    public  void MoveKeys(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public static Vector3 Move()
    {
        Vector3 _move = new Vector3(x, 0, y);
        return _move;
    }

    void ChangeButtonBehavior(Transform t,float f)
    {
        if (t==null&&(PlayerEvents.weopenType!=WeopenType.Disc))
        {
            
            KillButton.interactable = false;

        }
        else
        {
            switch (PlayerEvents.weopenType)
            {
                case WeopenType.none:
                    if (f <= 2)
                    {
                        KillButton.interactable = true;
                    }
                    else
                    {
                        KillButton.interactable = false;
                    }
                    break;
                case WeopenType.Knife:
                    if (f <= 2.5f)
                    {
                        KillButton.interactable = true;
                    }
                    else
                    {
                        KillButton.interactable = false;
                    }
                    break;
                case WeopenType.Butcher:
                    if (f <= 4f)
                    {
                        KillButton.interactable = true;
                    }
                    else
                    {
                        KillButton.interactable = false;
                    }
                    break;
                case WeopenType.Disc:
                    if (disableDiscButton==true)
                    {
                        KillButton.interactable = true;
                    }
                        
                    break;

                
                default:
                    break;
            }


           
            
            
           
        }
    }

    void StartKillEvent()
    {
        /*if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetTrigger("attack");
        }*/
    }

    #region UI
    public void OnClickKillEvent()
    {
        if (EventController.startKillEvent!=null)
        {
            EventController.startKillEvent();
        }
        if (PlayerEvents.weopenType!=WeopenType.Disc)
        {
            StartCoroutine(StartCaroutineKillEvent(0.5f));
        }
        if (PlayerEvents.weopenType == WeopenType.Disc)
        {
            StartCoroutine(DisableButton());
           // StartCoroutine(StartCaroutineKillEvent(0.02f));
            
            
        }
       
    }
    #endregion

    bool disableDiscButton;
    IEnumerator DisableButton()
    {
        disableDiscButton = false;
        KillButton.interactable = false;
        yield return new WaitForSeconds(0.1f);
        disableDiscButton = true;
        KillButton.interactable = true;

    }
    IEnumerator StartCaroutineKillEvent(float time)
    {
        x = 0;
        y = 0;
        stopMvt = false;
        yield return new WaitForSeconds(time);
        stopMvt = true;

    }

    void GameWin()
    {
        x = 0;
        y = 0;
        stopMvt = false;
    }
    void EnableTeleportButton(bool b,Transform t)
    {
        TeleportButton.SetActive(b);
        Teleport = t;
    }

    Transform Teleport;
    public void EnterToTeleport()
    {
        if (EventController.enterTeleport!=null)
        {
            EventController.enterTeleport(Teleport);
        }
        TeleportButton.SetActive(false);
    }

    
}
