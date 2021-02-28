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
    public GameObject Fleshe;
    private void OnEnable()
    {
        if (cinemachineBrain==null)
        {

            cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        }
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
            if (other.GetComponent<PlayerMovement>().enabled==false)
            {
                return;
            }
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
        cinemachineBrain.m_DefaultBlend.m_Time = 0.0f;
        if (t==transform)
        {
            if (Fleshe!=null)
            {
                Fleshe.SetActive(false);
            }
            Open.SetActive(true);
            Camera.SetActive(true);
            TeleportCanvas.SetActive(true);
        }
      
        Left.SetActive(false);
        //cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;
    }

    public void PlayerLeft()
    {

        // cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;
        StartCoroutine(RebackCinemachine());
        if (EventController.leftTeleport!=null)
        {
            EventController.leftTeleport(transform, TeleportPos);
        }
        TeleportCanvas.SetActive(false);
        Left.SetActive(true);
        Open.SetActive(false);
        Camera.SetActive(false);
    }

    IEnumerator RebackCinemachine()
    {
        yield return new WaitForSeconds(0.2f);
        cinemachineBrain.m_DefaultBlend.m_Time = 2.0f;
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
