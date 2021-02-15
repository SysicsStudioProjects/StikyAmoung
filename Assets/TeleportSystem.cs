using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class TeleportSystem : MonoBehaviour
{
    public GameObject TeleportCanvas;

    public GameObject Open;

    public Transform TeleportPos;

    public GameObject Left;
    public GameObject Camera;
    public CinemachineBrain cinemachineBrain;
    public TeleportSystem nextTeleport;
    public TeleportSystem backTeleport;
    private void OnEnable()
    {
        EventController.enterTeleport += PlayerEnter;
    }
    private void OnDisable()
    {
        EventController.enterTeleport -= PlayerEnter;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PLayer")
        {
            //hey hey we will send to enable Teleport Pos
            if (EventController.teleportCollision!=null)
            {
                EventController.teleportCollision(true,transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "PLayer")
        {
            //hey hey we will send to enable Teleport Pos
            if (EventController.teleportCollision != null)
            {
                EventController.teleportCollision(false,null);
            }
        }
    }


    void PlayerEnter(Transform t)
    {
        if (t==transform)
        {

            Open.SetActive(true);
            Camera.SetActive(true);
            TeleportCanvas.SetActive(true);
        }
      
        Left.SetActive(false);
        //cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;
    }

    public void PlayerLeft()
    {
        cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;
        if (EventController.leftTeleport!=null)
        {
            EventController.leftTeleport(transform, TeleportPos);
        }
        TeleportCanvas.SetActive(false);
        Left.SetActive(true);
        Open.SetActive(false);
        Camera.SetActive(false);
    }

    public void NextTeleport()
    {
        cinemachineBrain.m_DefaultBlend.m_Time = 0f;
        Camera.SetActive(false);
        nextTeleport.Camera.SetActive(true);
        nextTeleport.TeleportCanvas.SetActive(true);
        TeleportCanvas.SetActive(false);
    }

    public void BackTeleport()
    {
        cinemachineBrain.m_DefaultBlend.m_Time = 0f;
        Camera.SetActive(false);
        backTeleport.Camera.SetActive(true);
        backTeleport.TeleportCanvas.SetActive(true);
        TeleportCanvas.SetActive(false);
    }
}
