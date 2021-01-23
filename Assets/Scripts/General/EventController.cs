using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventController
{
    public delegate void SendPath(Pathe p);
    public static SendPath sendPath;

    public delegate void CanKill(Transform t);
    public static CanKill canKill;

    public delegate void StartKillEvent();
    public static StartKillEvent startKillEvent;
        
}
