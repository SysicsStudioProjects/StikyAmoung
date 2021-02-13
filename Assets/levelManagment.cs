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
        SceneManager.LoadSceneAsync(Singleton._instance.level);
    }

    public void OnchargeScene(){
        SceneManager.LoadSceneAsync(Singleton._instance.level);
    }
}
