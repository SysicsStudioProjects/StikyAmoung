using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelManagment : MonoBehaviour
{


    private void OnEnable()
    {
        
    }
    
    void Start(){
        print(Singleton._instance.level);
        StartCoroutine(StartScene());
    }

    public void OnchargeScene(){
        SceneManager.LoadSceneAsync(Singleton._instance.level);
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2.5f);
        //SceneManager.LoadSceneAsync(1);
        int a = 0;
        a = Singleton._instance.level / 5;
        int modulo = Singleton._instance.level % 5;
        if (modulo == 0)
        {
            SceneManager.LoadScene(a);
        }
        else
        {
            SceneManager.LoadScene(a+1);
        }

    }
}
