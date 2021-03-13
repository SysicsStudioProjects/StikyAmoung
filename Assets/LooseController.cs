using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LooseController : MonoBehaviour
{

    public Transform Player;
    public CinemachineBrain cinemachineBrain;

    public float time;
    public float rotateValue;

    public Transform startPos;
    public Transform finishPos;
    public Transform[] sprites;
    bool canRotate;
    private void OnEnable()
    {
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        cinemachineBrain.m_DefaultBlend.m_Time = 0.0f;
        StartCoroutine(RotatePlayer());
       // LeanTween.moveLocalX(Player.gameObject, 0, 1);
        // Player.transform.position = startPos.position;

        //Player.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].localPosition = new Vector3(sprites[i].localPosition.x + 0.01f, sprites[i].localPosition.y, sprites[i].localPosition.z);
            if (sprites[i].localPosition.x>=15.22f)
            {
                sprites[i].localPosition = new Vector3(-15.22f, sprites[i].localPosition.y, sprites[i].localPosition.z);
            }
        }
        if (canRotate)
        {
            Player.localPosition = Vector3.Lerp(Player.localPosition, new Vector3(0, Player.localPosition.y, Player.localPosition.z), 0.02f);
            Player.RotateAroundLocal(transform.forward, 0.02f);
            Player.RotateAroundLocal(Vector3.right, 0.02f);
        }
        // Player.rotation = Random.rotation;
      
    }

    IEnumerator RotatePlayer()
    {
        /*  float s = 0.015f;
          Vector3 dir = Player.transform.position - finishPos.position;
          for (int i = 0; i < 100; i++)
          {
              s -= 0.0001f;

              if (Player.localPosition.x>=0)
              {
                  time += 0.0001f;
                  Player.position = new Vector3(Player.position.x + s, Player.position.y, Player.position.z);
                  Player.RotateAroundLocal(transform.forward, 0.05f);
                  Player.RotateAroundLocal(Vector3.right, 0.05f);
              }
              else
              {

              }


              yield return new WaitForSecondsRealtime(time);

              // Player.Translate(dir * 0.02f);

          }*/

        yield return new WaitForSecondsRealtime(0.6f);
        canRotate = true;
        //StartCoroutine(RotatePlayer());
    }
}
