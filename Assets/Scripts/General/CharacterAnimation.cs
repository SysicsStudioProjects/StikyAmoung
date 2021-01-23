using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public GameObject Hands;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableHands()
    {
        Hands.SetActive(false);
    }
    public void ActivateHands()
    {
        Hands.SetActive(true);
    }
}
