using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventController
{
    public delegate void GameStart(bool b);
    public static GameStart gameStart;

    public delegate void SendPath(Pathe p);
    public static SendPath sendPath;

    public delegate void CanKill(Transform t, float raduis);
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
    public static IsBonuceLevel isBonuceLevel;

    public delegate void UseSkin(Skin s);
    public static UseSkin useSkin;

    public delegate void RemoveSkin(Skin s);
    public static RemoveSkin removeSkin;

    public delegate void SetPlayer(Transform p);
    public static SetPlayer setPlayer;

    public delegate void SwitchKin(Skin s);
    public static SwitchKin switchKin;

    public delegate void TeleportCollision(bool b, Transform t);
    public static TeleportCollision teleportCollision;

    public delegate void EnterTeleport(Transform t);
    public static EnterTeleport enterTeleport;

    public delegate void LeftTeleport(Transform teleport, Transform pos);
    public static LeftTeleport leftTeleport;

    public delegate void DeathWithLaser();
    public static DeathWithLaser deathWithLaser;

    public delegate void DeathWithSpike();
    public static DeathWithSpike deathWithSpike;
        

    public delegate void CardRequired(bool b);
    public static CardRequired cardRequired;

    public delegate void HasACard(CardType card);
    public static HasACard hasACard;

    //Ads System
    public delegate void VideoRewarded(bool Isrewarded);
    public static VideoRewarded videoRewarded;

    public delegate void ChnageButtonRewardRequest(bool b);
    public static ChnageButtonRewardRequest chnageButtonRewardRequest;


    public delegate void OnchangeItems();
    public static OnchangeItems onchangeItems;

    public delegate void GetSkinStart(Skin s);
    public static GetSkinStart getSkinstart;



}
