using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public List<Pathe> pathes;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        SetupEnnemiePath();
    }
    private void OnDisable()
    {
        
    }


    void SetupEnnemiePath()
    {
        foreach (var item in pathes)
        {
            if (EventController.sendPath != null)
            {
                EventController.sendPath(item);
            }
        }
    }
}
