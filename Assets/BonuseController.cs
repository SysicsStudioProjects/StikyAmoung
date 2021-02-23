using System.Collections;

using UnityEngine;
using UnityEngine.UI;
public class BonuseController : MonoBehaviour
{
    int coinsDefault;
    int coinsWin;
    public Text CoinsText;

    public void InitCoins(int c)
    {
        coinsWin = c;
        coinsDefault = Singleton._instance.coins;
        CoinsText.text = coinsDefault.ToString();
        StartCoroutine(CaroutineBonuse());
    }

   

   
    IEnumerator CaroutineBonuse()
    {
        yield return new WaitForSeconds(1.5f);
        int value = coinsWin / 20;
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForFixedUpdate();

            if (coinsWin <= 0)
            {
                Singleton._instance.coins = coinsDefault;
                Singleton._instance.save();
               
                break;
            }
            coinsWin -= value;
           
            coinsDefault += value;
            CoinsText.text = coinsDefault.ToString();

        }
    }
}
