using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Spin : MonoBehaviour
{
    public static Spin s;
    bool tour=true;
    int choix,nbAds;
    public int nbChoix;
    
    public Text date;
    DateTime clicked, dateWait;

    // Start is called before the first frame update
    void Start()
    {
        s = this;
        dateWait = new DateTime(2000, 1, 1, 4, 0, 0);
        if (clicked.Year != 1)
        {
            
            dateWait = dateWait - new TimeSpan((DateTime.Now.Hour - clicked.Hour), (DateTime.Now.Minute - clicked.Minute), (DateTime.Now.Second - clicked.Second));
            StartCoroutine(timer());
            tour = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    public void click()
    {
        if (tour)
        {
            Ads.ins_ads.nbvideo = 1;
            spinTourne();
            tour = false;
            clicked = DateTime.Now;
            StartCoroutine(timer());
        }
    }

    public void spinTourne()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        
        choix = UnityEngine.Random.Range(0, nbChoix);
        StartCoroutine(Tourne());
    }

    private IEnumerator Tourne()
    {
        for(int i=0; i < (choix+nbChoix*3)*5  ; i++)
        {
            transform.Rotate(new Vector3(0,0,1),360.0f/(nbChoix*5));
            yield return new WaitForSeconds(0.05f);
        }
        
    }
    private IEnumerator timer()
    {
        dateWait -= new TimeSpan(0, 0, 1);
        yield return new WaitForSeconds(1);
        if (dateWait.Year != 2000)
        {
            StopAllCoroutines();

            dateWait = new DateTime(2000, 1, 1, 4, 0, 0);
            clicked = new DateTime();
        }
        if (clicked.Year != 1)
        {
            date.text = dateWait.Hour + ":" + dateWait.Minute + ":" + dateWait.Second;
        }
        else
        {
            date.text = "tourne";
            tour = true;
        }
        StartCoroutine(timer());
        
    }

    public void video()
    {
        Ads.ins_ads.showRewardP("Turn_Complete");
        //ads.text = nbAds+"tour";
        
    }
    
}
