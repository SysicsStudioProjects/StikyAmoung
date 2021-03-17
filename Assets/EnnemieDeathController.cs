using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieDeathController : MonoBehaviour
{
    public List<EnnemyDeath> ennemyDeaths;
    EnnemyDeath switchennemydeath;
    public static EnnemieDeathController _instance;

    private void Awake()
    {
        _instance = this;
    }
    public void ActivateEnnemie(Color m,Transform t)
    {
        foreach (var item in ennemyDeaths)
        {
            if (item.statut==false)
            {
                switchennemydeath = item;
                item.ennemy.transform.position = t.position;
                item.ennemy.transform.rotation = t.rotation;
                item.deadEvent.SetColor(m);
                
                item.ennemy.SetActive(true);
                item.statut = true;
                StartCoroutine(DestactivateObj(item.ennemy));
                return;
            }
        }
    }

    public string hat;
    public string glasses;
    public void GetSskins(string h,string g)
    {
        hat = h;
        glasses = g;
      
    }

    IEnumerator DestactivateObj(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }
}

[System.Serializable]
public class EnnemyDeath
{
    public GameObject ennemy;
    public DeadEvent deadEvent;
    public bool statut;

}