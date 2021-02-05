using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieDeathController : MonoBehaviour
{
    public List<EnnemyDeath> ennemyDeaths;

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