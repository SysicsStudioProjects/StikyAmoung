using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public bool EnableCollider;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider other)
    {
        if (EnableCollider==false)
        {
            return;
        }
        if (other.transform.tag=="PLayer")
        {
          
            if (EventController.deathWithSpike!=null)
            {
                EventController.deathWithSpike();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (EnableCollider == false)
        {
            return;
        }
        if (other.transform.tag == "PLayer")
        {
            if (EventController.deathWithSpike != null)
            {
                EventController.deathWithSpike();
            }
        }
    }
}
