using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timeText;
    public int time;
    public float increment;
    bool isFinished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished==true)
        {
            return;
        }
        increment += Time.deltaTime;
        if (increment>=1)
        {
            time -= (int)increment;
            increment = 0;
            timeText.text = time.ToString();
        }
        if (time<=0)
        {
            time = 0;
            timeText.text = time.ToString();
            isFinished = true;
            if (EventController.levelBonuseFinished!=null)
            {
                EventController.levelBonuseFinished();
            }
        }
    }
}
