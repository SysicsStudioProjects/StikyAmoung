﻿
using UnityEngine;

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
    
    public float speedRotation;
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("speed"))
        {
            playerSpeed = PlayerPrefs.GetFloat("speed");
        }
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
      
    }

    void FixedUpdate()
    {
        //Application.targetFrameRate = 30;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //mo= new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = MovementController.Move();
       

        anim.SetFloat("speed", Mathf.Abs(move.magnitude * Time.deltaTime * playerSpeed));
        if (move!=Vector3.zero)
        {
            /*if (move.magnitude>0.8)
            {
                anim.speed = 1.5f;
                playerSpeed = 8;
            }
            if (move.magnitude<0.8&&move.magnitude>0.3)
            {
                anim.speed = 1;
                playerSpeed = 12;
            }
            if (move.magnitude<=0.3&&move.magnitude>0.05f)
            {
                anim.speed = 0.8f;
                playerSpeed =15;
            }
            if (move.magnitude<=0.01)
            {
                anim.speed = 1;
            }*/
            playerSpeed = 9;
            anim.speed = 2;
            controller.Move(move.normalized * Time.smoothDeltaTime * playerSpeed);
            // anim.speed = move.magnitude * Time.deltaTime * playerSpeed*10;
        }
        else
        {
            anim.speed = 1;
        }

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

        void ChangeTarget(Transform t,float raduis)
    {
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
    void GameWin()
    {
        transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
        this.enabled = false;
    }
    void GameLoose()
    {
        this.enabled = false;
    }
}
