﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEvent : MonoBehaviour
{
    bool isclicked;
    public Rigidbody rb;
    float speed;
    private void Start()
    {
        speed = Random.Range(40, 50);
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
        yield return new WaitForSeconds(0.7f);
        rb.isKinematic = true;
    }
}
