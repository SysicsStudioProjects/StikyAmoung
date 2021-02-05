using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovementController : MonoBehaviour
{
    public bool moveK;
    public bool MoveJ;
    public Joystick js;
    //send x joystic pos
   static float x;
    //send y joystic pos
  static  float y;
    public Button KillButton;
    bool stopMvt;
    private void OnEnable()
    {
        stopMvt = true;
        EventController.canKill += ChangeButtonBehavior;
        EventController.gameWin += GameWin;
    }

    private void OnDisable()
    {
        EventController.canKill -= ChangeButtonBehavior;
        EventController.gameWin -= GameWin;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }

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
        if (t==null)
        {
            KillButton.interactable = false;
        }
        else
        {
            if (f<=2)
            {
                KillButton.interactable = true;
            }
            else
            {
                KillButton.interactable = false;
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
        StartCoroutine(StartCaroutineKillEvent());
    }
    #endregion

    IEnumerator StartCaroutineKillEvent()
    {
        x = 0;
        y = 0;
        stopMvt = false;
        yield return new WaitForSeconds(0.5f);
        stopMvt = true;

    }

    void GameWin()
    {
        x = 0;
        y = 0;
        stopMvt = false;
    }
}
