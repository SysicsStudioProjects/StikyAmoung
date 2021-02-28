using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEvent : MonoBehaviour
{
    bool isclicked;
    public Rigidbody rb;
    float speed;

    public MeshRenderer UP;
    public MeshRenderer Down;

    public CreateMoney createMoney;


    private void Start()
    {
        speed = Random.Range(20, 40);
        if (LevelManager._instance.thislevel.IsBonuceLevel==true)
        {
            createMoney.enabled = true;
        }
        else
        {
            createMoney.enabled = false;
        }
        StartCoroutine(Retouche());
    }
  /*  #region UI
    public void OnclickEvent()
    {
        isclicked = true;
        //disable touch for ground
        if (EventsController.disableTouch != null)
        {
            EventsController.disableTouch(false);

        }
      

        if (EventsController.activateSac!=null)
        {
            EventsController.activateSac();
            Destroy(gameObject, 0.15f);
        }

        if (EventsController.hasAsack != null)
        {
            EventsController.hasAsack(true);

        }
    }



    #endregion*/
    private void Update()
    {
        rb.AddForce(-transform.up * speed, ForceMode.Acceleration);
        if (isclicked==true)
        {
            Vector3 scale = transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
            transform.localScale = scale;
        }
    }
    IEnumerator Retouche()
    {
        yield return new WaitForSeconds(2f);
        rb.isKinematic = true;
    }

    public void SetColor(Color c)
    {
        UP.materials[0].color = c;
        Down.materials[1].color = c;

    }
}
