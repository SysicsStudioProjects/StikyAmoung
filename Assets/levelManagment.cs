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
        yield return new WaitForSeconds(3.4f);
        SceneManager.LoadSceneAsync(Singleton._instance.level);
    }
}
