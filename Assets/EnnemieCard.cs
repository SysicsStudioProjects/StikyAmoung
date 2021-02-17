using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnnemieCard : MonoBehaviour
{
    public CardType cardtype;
    public Image image;
    public Sprite RedSprite;
    public Sprite BlueSprite;
    public Sprite WhiteSprite;


    private void OnEnable()
    {
        switch (cardtype)
        {
            case CardType.red:
                image.sprite = RedSprite;
                break;
            case CardType.blue:
                image.sprite = BlueSprite;

                break;
            case CardType.white:
                image.sprite = WhiteSprite;

                break;
            default:
                break;
        }
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
