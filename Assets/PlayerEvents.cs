﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{

	public string enemyTag;
	public float range;
	public Transform target;
	
	public Animator anim;
	public GameObject DeathPlayer;
    // Start is called before the first frame update
    private void OnEnable()
    {
		EventController.startKillEvent += Kill;
    }

    private void OnDisable()
    {
		EventController.startKillEvent -= Kill;

	}

	// Update is called once per frame
	void Update()
    {
		UpdateTarget();
        if (target!=null)
        {
			LookTotarget();
            
		}

        

	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			//targetEnemy = nearestEnemy.GetComponent<Enemy>();
		}
		else
		{
			target = null;
		}
        if (EventController.canKill!=null)
        {
			EventController.canKill(target);
        }
	}

	void LookTotarget()
    {
		 Vector3 dir = target.position - transform.position;
		 Quaternion lookRotation = Quaternion.LookRotation(dir);
		 Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.3f).eulerAngles;
		// LeanTween.rotate(transform, new Vector3(0,dir.y,0), 0.3f);
		 transform.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	void Kill()
    {
		anim.SetTrigger("attack");
		

    }
	public void KillEvent()
    {
        if (target!=null)
        {
			Destroy(target.gameObject, 0.2f);
			GameObject obj = Instantiate(DeathPlayer, target.position, transform.rotation);
			Destroy(obj, 2);
		}
		
	}
}
