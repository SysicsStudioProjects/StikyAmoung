using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMoney : MonoBehaviour
{
    public GameObject Money;
    // Start is called before the first frame update
    private void OnEnable()
    {
        
            Instantiate(Money, transform.position+ new Vector3(4,0,4), transform.rotation);
            Instantiate(Money, transform.position+ new Vector3(0,0,8), transform.rotation);
            Instantiate(Money, transform.position+ new Vector3(-4,0,-4), transform.rotation);
            Instantiate(Money, transform.position+ new Vector3(0,0,-8), transform.rotation);
        
       
    }


}
