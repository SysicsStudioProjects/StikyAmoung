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

    public List<GameObject> hats;
    public List<GameObject> glasses;

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
        SetupSkin();
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

    public void SetupSkin()
    {
       string h = EnnemieDeathController._instance.hat;
        string g = EnnemieDeathController._instance.glasses;
        int indexhat = hats.FindIndex(d => d.name == h);
        if (indexhat != -1)
        {
            hats[indexhat].SetActive(true);
        }
        int indexglasses = glasses.FindIndex(d => d.name == g);
        if (indexglasses != -1)
        {
            glasses[indexglasses].SetActive(true);
        }
        EnnemieDeathController._instance.hat = "";
        EnnemieDeathController._instance.glasses = "";

    }
}
