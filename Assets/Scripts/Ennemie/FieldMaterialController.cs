using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMaterialController : MonoBehaviour
{
    public FieldOfView view;
    public Color redColor;
    public Color greenColor;
    bool startColor;
    private void Update()
    {
        if (view.visibleTargets.Count>0 && startColor==false)
        {
            ALert();
           
        }
    }

    void ALert()
    {
        StartCoroutine(startChangeColor());
    }

    IEnumerator startChangeColor()
    {
        startColor = true;
        LeanTween.color(gameObject, redColor, 0.1f).setEaseLinear();
        yield return new WaitForSeconds(0.2f);
        LeanTween.color(gameObject, greenColor, 0.1f).setEaseLinear();
        yield return new WaitForSeconds(0.2f);
        if (view.visibleTargets.Count <= 0)
        {
            //LeanTween.color(gameObject, greenColor, 0.02f);
            startColor = false;
           
        }
        else
        {
            ALert();
        }
    }
}
