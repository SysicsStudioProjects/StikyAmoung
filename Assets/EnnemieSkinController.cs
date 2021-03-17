using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieSkinController : MonoBehaviour
{

    public List<GameObject> Glasses;
    public List<GameObject> Hats;
    string g;
    string h;
    private void OnEnable()
    {
        for (int i = 0; i < Glasses.Count; i++)
        {
            Glasses[i].SetActive(false);
        }
        for (int i = 0; i < Hats.Count; i++)
        {
            Hats[i].SetActive(false);
        }
        int hatindex = Random.Range(0, Hats.Count);
        int glassesIndex = Random.Range(0, Glasses.Count);
        Glasses[glassesIndex].SetActive(true);
        g = Glasses[glassesIndex].name;
        Hats[hatindex].SetActive(true);
        h = Hats[hatindex].name;

    }

    private void OnDisable()
    {
        EnnemieDeathController._instance.GetSskins(h, g);
    }
}
