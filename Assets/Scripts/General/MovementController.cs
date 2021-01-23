using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool moveK;
    public bool MoveJ;
    public Joystick js;
   static float x;
  static  float y;
    //public delegate void Move(Vector3 v);
  //  public static event Move move;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveJ)
        {
            MoveJoystic(js.Horizontal,js.Vertical);
        }
        if (moveK)
        {
            MoveKeys(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
}
