using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnnemiePos : MonoBehaviour
{

    public static EnnemiePos _instance;
    public List<Transform> ennemiePos;

    private void Awake()
    {
        _instance = this;
    }
    public Transform RetutnPos()
    {
        int R = Random.Range(0, ennemiePos.Count);
        return ennemiePos[R];
    }
}
