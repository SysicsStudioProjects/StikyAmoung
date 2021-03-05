using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMvt : MonoBehaviour
{
    public Rigidbody rb;
    public Joystick js;
    public Animator anim;
    public float playerSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(js.Horizontal, 0, js.Vertical);
        rb.velocity = move.normalized * Time.deltaTime * playerSpeed;
        anim.SetFloat("speed", Mathf.Abs(move.magnitude * Time.deltaTime * playerSpeed));
        if (move!=Vector3.zero)
        {
            LockOnTarget(move.normalized);
        }
      
    }

    void LockOnTarget(Vector3 _target)
    {
       
            Quaternion startrotation = new Quaternion(0, 0, 0, 0);
            Vector3 root = Vector3.Lerp(transform.forward, _target, 0.6f);
            transform.forward = root;
        

    }
}
