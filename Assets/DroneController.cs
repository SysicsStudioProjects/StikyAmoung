using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public EnnemiePatrol ennemiePatrol;
    public FieldOfView view;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventController.gameLoose += GameLoose;
        EventController.gameWin += GameWin;
    }
    private void OnDisable()
    {
        EventController.gameLoose -= GameLoose;
        EventController.gameWin -= GameWin;
    }

    void GameWin()
    {
        ennemiePatrol.enabled = false;
        view.enabled = false;
    }
    void GameLoose()
    {
        ennemiePatrol.enabled = false;
        view.enabled = false;
    }
}
