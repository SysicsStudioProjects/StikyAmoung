using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDeath : MonoBehaviour
{
    public List<Sprite> deathsprites;
    public Image img;
    public float time;
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartCoroutine(ReplaceImage());
    }


    IEnumerator ReplaceImage()
    {
        for (int i = 0; i < deathsprites.Count; i++)
        {
            img.sprite = deathsprites[i];
            yield return new WaitForSecondsRealtime(time);
            

        }
        StartCoroutine(ReplaceImage());
    }
}
