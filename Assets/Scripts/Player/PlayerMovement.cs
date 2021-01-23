using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public Animator anim;
    bool ennemiisTarget;
    
    public float speedRotation;
    private void OnEnable()
    {
        EventController.canKill += ChangeTarget;

    }

    private void OnDisable()
    {
        EventController.canKill -= ChangeTarget;
    }

    private void Start()
    {
      
    }

    void Update()
    {
       
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //mo= new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = MovementController.Move();
        controller.Move(move * Time.deltaTime * playerSpeed);

        anim.SetFloat("speed", Mathf.Abs(move.magnitude * Time.deltaTime * playerSpeed));
        if (move != Vector3.zero)
        {
            LockOnTarget(move);
           // gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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

        void ChangeTarget(Transform t)
    {
        if (t==null)
        {
            ennemiisTarget = false;
        }
        else
        {
            ennemiisTarget = true;
        }
    }
    
}
