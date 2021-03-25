using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class Spin : MonoBehaviour
{
    
    bool tour=true;
    int choix;
    public int nbChoix;
    
    public Text date;
    DateTime clicked, dateWait;
    public GameObject AnimationMoney;
    public GameObject Timeobj;

    public Button FreeSpinButton;
    public Button DailyButton;

    public Text CoinsWin;
    bool isStartRotate;
    string type;
    public Button _watchButton;
    public Text _watchButtonText;
    public Text CoinsText;
    int CoinsValue;
    int AllCoins;
    public AudioSource audio;
    private void OnEnable()
    {
        inits();
        //Ads.tourads += spinTourne;
       // EventController.chnageButtonRewardRequest += ChangeRewardStatut;
        
        EventController.videoRewarded += VideoBonuseRewarded;
        //_watchButton.interactable = IronSource.Agent.isRewardedVideoAvailable();
        _watchButton.interactable = AdsManager._instance.VerifRewarded();
    }
    private void OnDisable()
    {
     //  EventController.chnageButtonRewardRequest -= ChangeRewardStatut;
        
        EventController.videoRewarded -= VideoBonuseRewarded;
        //Ads.tourads -= spinTourne;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void inits()
    {
        AllCoins = Singleton._instance.coins;
        CoinsText.text = AllCoins.ToString();

        if (Singleton._instance.AdsspinDate == null)
        {
            Singleton._instance.AdsspinDate = DateTime.Now;
            Singleton._instance.AdsSpinVideo = 5;
            Singleton._instance.save();
        }
        else
        {
            if (DateTime.Now - Singleton._instance.AdsspinDate > new TimeSpan(1, 0, 0, 0))
            {
                Singleton._instance.AdsspinDate = DateTime.Now;
                Singleton._instance.AdsSpinVideo = 5;
                Singleton._instance.save();
            }
        }
        if (Singleton._instance.spinDate != "")
        {
            clicked = DateTime.Parse(Singleton._instance.spinDate);
        }
        else
        {
            clicked = new DateTime(2000, 1, 1, 4, 0, 0);
        }

        print(clicked);
        dateWait = new DateTime(2000, 1, 1, 4, 0, 0);
        if (DateTime.Now - clicked < new TimeSpan(0, 4, 0, 0))
        {

            dateWait = dateWait - new TimeSpan((DateTime.Now.Hour - clicked.Hour), (DateTime.Now.Minute - clicked.Minute), (DateTime.Now.Second - clicked.Second));
            StartCoroutine(timer());
            tour = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (tour==false)
        {
            FreeSpinButton.interactable = false;
          
        }
        else
        {
            if (isStartRotate==false)
            {
                FreeSpinButton.interactable = true;
            }
            else
            {
                FreeSpinButton.interactable = false;
            }
        }
       if (isStartRotate==false && Singleton._instance.AdsSpinVideo>0 && AdsManager._instance.VerifRewarded())
        {
            DailyButton.interactable = true;
        }
        else
        {
            DailyButton.interactable = false;
        }
        switch (Singleton._instance.AdsSpinVideo)
        {
            case 5:
                _watchButtonText.text = "5 DAILY SPIN";
                break;

            case 4:
                _watchButtonText.text = "4 DAILY SPIN";
                break;
            case 3:
                _watchButtonText.text = "3 DAILY SPIN";
                break;
            case 2:
                _watchButtonText.text = "2 DAILY SPIN";
                break;
            case 1:
                _watchButtonText.text = "1 DAILY SPIN";
                break;
            case 0:
                _watchButtonText.text = "0 DAILY SPIN";
                break;

            default:
                break;
        }

    }
    public void click()
    {
        if (tour)
        {
            
            //Ads.ins_ads.nbvideo = 1;
            spinTourne();
            tour = false;
           
           
            print(clicked);
            type = "tourne";
          
        }
        
    }

    public void spinTourne()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        
        choix = UnityEngine.Random.Range(0, 6);
        StartCoroutine(Tourne());
    }

    private IEnumerator Tourne()
    {
        isStartRotate = true;
        audio.Play();
        for (int i=0; i < (choix+nbChoix*3)*10  ; i++)
        {
            
            transform.Rotate(new Vector3(0,0,1),360.0f/(nbChoix*10));
            if (i > (choix + nbChoix * 3) * 10 - 30)
            {
                yield return new WaitForSeconds(0.02f);
            }
           else if (i> (choix + nbChoix * 3) * 10-10)
            {
                yield return new WaitForSeconds(0.02f*3.0f);
            }
            else if (i > (choix + nbChoix * 3) * 10 - 5)
            {
                yield return new WaitForSeconds(0.02f * 4.0f);
            }
            else
            {
                // yield return new WaitForSeconds(0.02f);
                yield return new WaitForFixedUpdate();
            }
           
        }
        print(choix);
        audio.Stop();
        AnimationMoney.SetActive(true);
        switch (choix)
        {
            case 0:
                //you win 200
                CoinsWin.text = "+" + 200;
                CoinsValue = 200;

                break;
            case 1:
                //you win 400
                CoinsWin.text = "+" + 400;
                CoinsValue = 400;



                break;
            case 2:
                //you win 600
                CoinsWin.text = "+" + 600;
                CoinsValue = 600;



                break;
            case 3:
                //you win 500
                CoinsWin.text = "+" + 500;
                CoinsValue = 500;




                break;
            case 4:
                //you win 600
                CoinsWin.text = "+" + 600;
                CoinsValue = 600;




                break;
            case 5:
                //you win 300
                CoinsWin.text = "+" + 300;
                CoinsValue = 300;



                break;
            case 6:
                //you win 800
                CoinsWin.text = "+" + 800;
                CoinsValue = 800;



                break;
            case 7:
                //you win 1200
                CoinsWin.text = "+" + 1200;
                CoinsValue = 1200;


                break;
            default:
                break;
        }

        StartCoroutine(SetupCoin());
    }

    IEnumerator SetupCoin()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        int value = CoinsValue / 20;
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSecondsRealtime(0.04f);

            if (CoinsValue <= 0)
            {
                CoinsValue = 0;
                break;
            }
            CoinsValue -= value;
            AllCoins += value;
            CoinsText.text = AllCoins.ToString();

        }
    }
        private IEnumerator timer()
    {
        Timeobj.SetActive(true);
        dateWait -= new TimeSpan(0, 0, 1);
        yield return new WaitForSecondsRealtime(1);
        /*if (dateWait.Year != 2000)
        {
            StopAllCoroutines();
            Timeobj.SetActive(false);
            date.text = "tourne";

            dateWait = new DateTime(2000, 1, 1, 0, 0, 10);
            clicked = new DateTime();
        }*/
        if (DateTime.Now - clicked < new TimeSpan(0, 4, 0, 0))
        {
            if (dateWait.Hour<=0)
            {
                if (dateWait.Minute<0)
                {
                    date.text =  "00" + ":" + dateWait.Second;
                }
                else
                {
                    if (dateWait.Minute>=10)
                    {
                        date.text = dateWait.Minute + ":" + dateWait.Second;
                    }
                    else
                    {
                        date.text = "0"+dateWait.Minute + ":" + dateWait.Second;
                    }
                   
                }
            }
            else
            {
                date.text = dateWait.Hour + ":" + dateWait.Minute + ":" + dateWait.Second;
            }
            StartCoroutine(timer());
        }
        else
        {
            Timeobj.SetActive(false);
            print("we are here");
            AnimationMoney.SetActive(false);
            date.text = "tourne";
            tour = true;
            FreeSpinButton.interactable = true;



        }
        
        
    }

    public void video()
    {
        //Ads.ins_ads.showRewardP("Turn_Complete");
        //ads.text = nbAds+"tour";
        
    }
    public void videoDeffaut()
    {
       // Ads.ins_ads.showReward();
        

    }
    public void Ok()
    {

        isStartRotate = false;
       
        Getchoix();
        if (type=="tourne")
        {
            
            clicked = DateTime.Now;
            Singleton._instance.spinDate = clicked.ToString();
            Timeobj.SetActive(true);
            StartCoroutine(timer());
        }
        type = "";
        Singleton._instance.save();




    }
   void Getchoix()
    {
        
        switch (choix)
        {
            case 0:
                //you win 200
                CoinsWin.text="+" + 200;
                Singleton._instance.coins += 200;
                Singleton._instance.save();
                break;
            case 1:
                //you win 400
                CoinsWin.text = "+" + 400;
                Singleton._instance.coins += 400;
                Singleton._instance.save();


                break;
            case 2:
                //you win 600
                CoinsWin.text = "+" + 600;
                Singleton._instance.coins += 600;
                Singleton._instance.save();


                break;
            case 3:
                //you win 500
                CoinsWin.text = "+" + 500;
                Singleton._instance.coins += 500;
                Singleton._instance.save();


                break;
            case 4:
                //you win 600
                CoinsWin.text = "+" + 600;
                Singleton._instance.coins += 600;
                Singleton._instance.save();


                break;
            case 5:
                //you win 300
                CoinsWin.text = "+" + 300;
                Singleton._instance.coins += 300;
                Singleton._instance.save();


                break;
            case 6:
                //you win 800
                CoinsWin.text = "+" + 800;
                Singleton._instance.coins += 800;
                Singleton._instance.save();


                break;
            case 7:
                //you win 1200
                CoinsWin.text = "+" + 1200;
                Singleton._instance.coins += 1200;
                Singleton._instance.save();


                break;
            default:
                break;
        }
    }

    public void OnclickVideo()
    {
        ShowVideo();
        
        Singleton._instance.AdsSpinVideo -= 1;
        Singleton._instance.save();
    }
    public void ChangeRewardStatut(bool b)
    {
        _watchButton.interactable = b;
    }

    bool IsWatched;
    void ShowVideo()
    {
       
        IsWatched = true;
        //AdsManager._instance.ShowRewardVideo("Luckywheelscreen_freespin_reward");
        AdsManager._instance.ShowReward("Luckywheelscreen_freespin_reward");
    }

    void VideoBonuseRewarded(bool b)
    {
        if (b == false)
        {
            IsWatched = false;
            return;
        }
        if (IsWatched)
        {
            spinTourne();
        }

        IsWatched = false;
    }
       
    

    }




