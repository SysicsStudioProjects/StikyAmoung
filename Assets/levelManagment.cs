using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelManagment : MonoBehaviour
{


    private void OnEnable()
    {
        SceneManager.LoadSceneAsync(Singleton._instance.level);
    }
}
