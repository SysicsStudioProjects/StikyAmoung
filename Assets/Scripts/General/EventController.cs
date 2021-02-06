using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventController
{
    public delegate void GameStart();
    public static GameStart gameStart;

    public delegate void SendPath(Pathe p);
    public static SendPath sendPath;

    public delegate void CanKill(Transform t,float raduis);
    public static CanKill canKill;

    public delegate void StartKillEvent();
    public static StartKillEvent startKillEvent;

    public delegate void EnnemieDown(EnnemiePatrol obj);
    public static EnnemieDown ennemieDown;

    public delegate void SendSettingData(float speed, float ennemydetect, bool autofocuse, bool vibration);
    public static SendSettingData sendSettingData;

    public delegate void LevelBonuseFinished();
    public static LevelBonuseFinished levelBonuseFinished;

    public delegate void GameWin();
    public static GameWin gameWin;

    public delegate void GameLoose();
    public static GameLoose gameLoose;

    public delegate void IsBonuceLevel(bool b);
    public static IsBonuceLevel isBonuceLevel ;

    public delegate void UseSkin(Skin s);
    public static UseSkin useSkin;

    public delegate void RemoveSkin(Skin s);
    public static RemoveSkin removeSkin;

    public delegate void SetPlayer(Transform p);
    public static SetPlayer setPlayer;

    public delegate void SwitchKin(Skin s);
    public static SwitchKin switchKin;
}
