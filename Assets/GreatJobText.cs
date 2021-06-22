using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GreatJobText : MonoBehaviour
{
    public Text text;
    string great = "GREAT JOB";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(caroutineText());
    }

    IEnumerator caroutineText()
    {
        text.text = "";
        for (int i = 0; i < great.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            text.text += great[i];
        }
    }
}
