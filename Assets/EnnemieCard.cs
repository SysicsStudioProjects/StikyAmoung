using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieCard : MonoBehaviour
{
    public CardType cardtype;



    private void OnEnable()
    {
        EventController.gameStart+=GameStart;
    }
   
    
    private void OnDisable()
    {
        EventController.gameStart-=GameStart;

        if(IsGameStarted){
            if (EventController.hasACard!=null)
        {
            EventController.hasACard(cardtype);
        }
        }
        
    }


    bool IsGameStarted;

    void GameStart(bool b){
        IsGameStarted=b;
    }
}
