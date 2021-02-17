using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorVelocity : MonoBehaviour
{
    public Rigidbody rb;
    Transform target;
    PlayerMovement p;
    CharacterController ch;
    Vector3 forward;
    public AudioSource audio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(transform.up, 3);

        if (target!=null)
        {
            ch.Move(-forward * 25 * Time.deltaTime);
        }

        // rb.angularVelocity = transform.up * 10;
       // rb.angularVelocity = rb.transform.up * 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="PLayer")
        {
            print("Heyyyyy");
            //   collision.transform.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward* 2000,transform.position+Vector3.one, ForceMode.Force);

            forward = transform.forward;
            target = collision.transform;
            p=target.GetComponent<PlayerMovement>();
            p.PlayBrawling();
            ch = target.GetComponent<CharacterController>();
            audio.Play();
            StartCoroutine(MovePlayer());
        }
    }

    IEnumerator MovePlayer()
    {
        p.enabled = false;
        yield return new WaitForSeconds(0.7f);
        audio.Stop();
        p.enabled = true;
        target = null;
        ch = null;
        
    }
}
