﻿
using UnityEngine;
using UnityEngine.AI;
public class EnnemeieBonuse : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    public Animator anim;

    public SkinnedMeshRenderer BodyRendered;
    public SkinnedMeshRenderer HandLeftRendere;
    public SkinnedMeshRenderer HandRightRendere;
    public Color MaterialColor;
    // Use this for initialization
    void OnEnable()
    {
        SetupMaterial();
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void SetupMaterial()
    {
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", MaterialColor);
        BodyRendered.materials[0].color = MaterialColor;
        HandLeftRendere.SetPropertyBlock(block);
        HandRightRendere.SetPropertyBlock(block);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        anim.SetFloat("speed", agent.velocity.magnitude);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void DetectPlayer(Transform t)
    {
       

    }
    private void OnDisable()
    {
        if (EventController.ennemieDown != null)
        {
            EventController.ennemieDown(null);
        }

    }
    private void OnDestroy()
    {
       
        
    }
}
