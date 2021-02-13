using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollManual : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject obj;

    public Transform content;

    public float hight;

    public  float DistanceToRecalcVisibility = 400.0f;
    public  float DistanceMarginForLoad = 600.0f;
    private float lastPos = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {
        /* this.scrollRect.onValueChanged.AddListener((newValue) => {
             if (Mathf.Abs(this.lastPos - this.scrollRect.content.transform.localPosition.x) >= DistanceToRecalcVisibility)
             {
                 lastPos = this.scrollRect.content.transform.localPosition.x;

                 Vector2 scrollRectPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, this.scrollRect.transform.position);
                 RectTransform scrollRectTransform = this.scrollRect.GetComponent<RectTransform>();
                 float checkRectMinY = scrollRectPosition.x + scrollRectTransform.rect.xMin - DistanceMarginForLoad;
                 float checkRectMaxY = scrollRectPosition.x + scrollRectTransform.rect.xMax + DistanceMarginForLoad;

                 foreach (Transform child in this.scrollRect.content)
                 {
                     Vector2 childPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, child.position);
                     // uncomment lines bellow if you set DistanceMarginForLoad less than height of single element
                     //RectTransform childRectTransform = child.GetComponent<RectTransform>();
                     //float childMinY = childPosition.y + childRectTransform.rect.yMin;
                     //float childMaxY = childPosition.y + childRectTransform.rect.yMax;
                     //if (childMaxY >= checkRectMinY && childMinY <= checkRectMaxY) {

                     if (childPosition.x >= checkRectMinY && childPosition.x <= checkRectMaxY)
                     {
                         child.GetComponent<Image>().color = Color.blue;
                     }
                     else
                     {
                         child.GetComponent<Image>().color = Color.green;
                     }
                 }
             }
         });*/

    }

    private void OnEnable()
    {
        scrollRect.normalizedPosition = new Vector2(1, hight);
        StartCoroutine(makeItsmoth());
    }

    // Update is called once per frame
    void Update()
    {

      
    }

    public  void ScrollToTop()
    {
        //scrollRect.normalizedPosition = new Vector2(1, 0);
    }
    IEnumerator makeItsmoth()
    {
        float a = 1;
        for (int i = 11; i > 0; i--)
        {
            a -= 0.1f;
            yield return new WaitForSeconds(0.03f);
            scrollRect.normalizedPosition = new Vector2(a, hight);
        }
        
    }
}
