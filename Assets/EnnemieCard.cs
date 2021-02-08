using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieCard : MonoBehaviour
{
    public CardType cardtype;

    private void OnDisable()
    {
        if (EventController.hasACard!=null)
        {
            EventController.hasACard(cardtype);
        }
    }
}
