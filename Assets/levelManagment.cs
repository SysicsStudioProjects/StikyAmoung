using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManagment : MonoBehaviour
{
    public GameObject TermsOf;
    public GameObject Animation;
    bool isAccepted;
    public string url;

    private void OnEnable()
    {
        isAccepted = PlayerPrefs.HasKey("terms");
    }
    
    void Start(){
        print(Singleton._instance.level);
        if (isAccepted)
        {
            Animation.gameObject.SetActive(true);
            StartCoroutine(StartScene());
        }
        else
        {
            if (TermsOf != null)
            {
                TermsOf.SetActive(true);
            }
        }

    }

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }

    public void AcceptTerms()
    {
        PlayerPrefs.SetInt("terms", 1);
        Animation.gameObject.SetActive(true);
        TermsOf.SetActive(false);
        StartCoroutine(StartScene());
    }

    public void OnchargeScene(){
        SceneManager.LoadSceneAsync(Singleton._instance.level);
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadSceneAsync(1);
        /*  int a = 0;
          a = Singleton._instance.level / 5;
          int modulo = Singleton._instance.level % 5;
          if (modulo == 0)
          {
              SceneManager.LoadScene(a);
          }
          else
          {
              SceneManager.LoadScene(a+1);
          }*/

        int a = Singleton._instance.level;
        SceneManager.LoadScene(a);

    }
}
