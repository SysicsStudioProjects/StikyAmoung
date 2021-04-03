using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using UnityEngine.SceneManagement;
public class FireBaseProcess : MonoBehaviour
{
    // Start is called before the first frame update
    private int _sceneIndex;

    private string _scenename;

    void Start()
    {
        var activeScene = SceneManager.GetActiveScene();
        _sceneIndex = activeScene.buildIndex - 1;
        _scenename = activeScene.name;
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,new Parameter(FirebaseAnalytics.ParameterLevel,_sceneIndex),new Parameter(FirebaseAnalytics.ParameterLevelName,_scenename));
    }

    private void OnDestroy()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, new Parameter(FirebaseAnalytics.ParameterLevel,_sceneIndex),new Parameter(FirebaseAnalytics.ParameterLevelName,_scenename));
        

    }
}
