using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleController : MonoBehaviour
{
    public string imposterStrig;
    public Text Title;
    private void OnEnable()
    {
        StartCoroutine(Write());
    }

    IEnumerator Write()
    {
        yield return new WaitForSecondsRealtime(2.8f);
        for (int i = 0; i < imposterStrig.Length; i++)
        {
            Title.text += imposterStrig[i];
            yield return new WaitForSecondsRealtime(0.07f);
        }
    }
}
