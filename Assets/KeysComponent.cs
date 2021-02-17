using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysComponent : MonoBehaviour
{
    // Start is called before the first frame update

    public GameControl gameControl;
    int nbKey;
    int ActiveKey;
    public List<Image> AmountImage;
    public AudioSource audio;
    private void OnEnable()
    {
        if (gameControl.LevelBonuse)
        {
            this.gameObject.SetActive(false);
            return;
        }
        nbKey = gameControl.LevelIndex;
        ActiveKey = nbKey % 5;
        print(ActiveKey);
        StartCoroutine(startCaroutine());

    }
    
    IEnumerator startCaroutine()
    {
        for (int i = 0; i < AmountImage.Count; i++)
        {
            if (i >= ActiveKey - 1)
            {
                AmountImage[i].gameObject.SetActive(false);
            }
            else
            {
                AmountImage[i].gameObject.SetActive(true);
                AmountImage[i].fillAmount = 1;

            }

        }
        
        yield return new WaitForSecondsRealtime(0.1f);
        AmountImage[ActiveKey-1].gameObject.SetActive(true);
        audio.Play();
        for (int i = 0; i < 10000; i++)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            AmountImage[ActiveKey-1].fillAmount += 0.05f;
        }
    }
}
