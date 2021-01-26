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

    public delegate void EnnemieDown(EnnemiePatrol obj);
    public static EnnemieDown ennemieDown;

    public delegate void SendSettingData(float speed, float ennemydetect, bool autofocuse, bool vibration);
    public static SendSettingData sendSettingData;

    public delegate void LevelBonuseFinished();
    public static LevelBonuseFinished levelBonuseFinished;

    public delegate void GameLoose();
    public static GameLoose gameLoose;
}
