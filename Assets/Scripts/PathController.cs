using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public List<Pathe> pathes;
    public List<EnnemiePatrol> ennemeis;
    
    private void OnEnable()
    {
        EventController.ennemieDown += EnnemyDown;
    }
    private void Start()
    {
        SetupEnnemiePath();
    }
    private void OnDisable()
    {
        EventController.ennemieDown -= EnnemyDown;

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

    void EnnemyDown(EnnemiePatrol ennemie)
    {
       
        ennemeis.Remove(ennemie);
        StartCoroutine(ChangeEnnemieBehavior());
    }
    IEnumerator ChangeEnnemieBehavior()
    {
        int index = pathes.FindIndex(e => e.isAlive == false);
        print(index);
        yield return new WaitForSeconds(0.5f);
        foreach (var item in ennemeis)
        {
            if (item.EnnemieId<0)
            {
                if (index!=-1)
                {
                    item.EnnemieId = pathes[index].idEnnemie;
                    pathes[index].isAlive = true;
                    SetupEnnemiePath();
                    break;
                }
                
            }
        }
    }
}
