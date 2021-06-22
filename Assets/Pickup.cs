using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Range(1,10)]
    public float RotateSpeed;

    public Skin thisskin;
    public GameObject PowerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDisable()
    {
        PowerUp.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, RotateSpeed);
    }
}
