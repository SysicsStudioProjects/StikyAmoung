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

    
    public float speedRotation;
    private void OnEnable()
    {
        //MovementController.move += Move;
    }

    private void OnDisable()
    {
        
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
        print(move.z + move.x);
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
        Quaternion startrotation = new Quaternion(0, 0, 0, 0);
        // Vector3 dir = _target - transform.position;
        // Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.5f).eulerAngles;
        //  LeanTween.rotate(BaseRotate.gameObject, new Vector3(0,dir.y,0), smoothtime);
        // transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        Vector3 root = Vector3.Lerp(transform.forward, _target, speedRotation);
        transform.forward = root;
    }
}
