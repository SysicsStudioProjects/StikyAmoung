﻿using System.Collections;

using UnityEngine;

public class PlayerEvents : MonoBehaviour
{

	public string enemyTag;
	public float range;
	public Transform target;
	
	public Animator anim;
	public GameObject DeathPlayer;
	public GameObject DeathBonucePlayer;
	public bool AutoFocuse;
	public bool vibration;

	bool isBonuceLevel;

	public GameObject BloodObj;

	public static WeopenType weopenType;
	
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (EventController.setPlayer!=null)
        {
			EventController.setPlayer(transform);
		}
		
		EventController.isBonuceLevel += VerifeLevel;
		
		
		EventController.startKillEvent += Kill;
		EventController.sendSettingData += GetSettingData;
		EventController.deathWithLaser += DeathWithLaser;
		EventController.deathWithSpike += DeathWithSpike;
		EventController.gameWin += GameWin;

	}

	private void OnDisable()
    {
		EventController.isBonuceLevel -= VerifeLevel;

		EventController.startKillEvent -= Kill;
		EventController.sendSettingData -= GetSettingData;
		EventController.deathWithLaser -= DeathWithLaser;
		EventController.deathWithSpike -= DeathWithSpike;
		EventController.gameWin -= GameWin;




	}

	// Update is called once per frame
	void Update()
    {
		UpdateTarget();
        if (target!=null&&AutoFocuse)
        {
			LookTotarget(0.3f);
            
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
        if (stopKilling==false)
        {
            if (switchTarget!=null)
            {
				if (EventController.canKill != null)
				{
					EventController.canKill(target, shortestDistance);
				}
			}
			switchTarget = target;


		}
        
	}
	Transform switchTarget;
	void LookTotarget(float speedrotate)
    {
		 Vector3 dir = target.position - transform.position;
		 Quaternion lookRotation = Quaternion.LookRotation(dir);
		 Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, speedrotate).eulerAngles;
		// LeanTween.rotate(transform, new Vector3(0,dir.y,0), 0.3f);
		 transform.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

	void Kill()
    {
        switch (weopenType)
        {
            case WeopenType.none:
				anim.SetTrigger("attack");
				break;
            case WeopenType.Knife:
				anim.SetTrigger("attackknife");

				break;
            case WeopenType.Disc:
				anim.SetTrigger("attackdisc");

				break;
			case WeopenType.Butcher:
				anim.SetTrigger("attackButcher");
				break;
            default:
                break;
        }
        
		

    }
	bool stopKilling;
	public void KillEvent(Transform t)
    {
        if (target!=null)
        {
			stopKilling = true;
			StartCoroutine(SlowTime());
			if (EventController.canKill != null)
			{
				EventController.canKill(null,0);
			}
			if (vibration)
            {
				Handheld.Vibrate();
			}
			
			LookTotarget(1);

			//	StartCoroutine(DesactivateEnnemy(target.gameObject));
			target.gameObject.SetActive(false);

			 if (isBonuceLevel)
			  {
				EnnemieDeathController._instance.ActivateEnnemie(target.GetComponent<EnnemeieBonuse>().MaterialColor, target);
			}
			  else
			{
				if(target.GetComponent<EnnemiePatrol>()!=null){
				EnnemieDeathController._instance.ActivateEnnemie(target.GetComponent<EnnemiePatrol>().MaterialColor, target);
				}
				else if(target.GetComponent<EnnemeieBonuse>()!=null){
				EnnemieDeathController._instance.ActivateEnnemie(target.GetComponent<EnnemeieBonuse>().MaterialColor, target);


				}
			}

			
		
			
		}
        if (t!=null)
        {
			
			if (EventController.canKill != null)
			{
				EventController.canKill(null, 0);
			}
			if (vibration)
			{
				Handheld.Vibrate();
			}

			

			//	StartCoroutine(DesactivateEnnemy(target.gameObject));
			t.gameObject.SetActive(false);

			if (isBonuceLevel)
			{
				EnnemieDeathController._instance.ActivateEnnemie(t.GetComponent<EnnemeieBonuse>().MaterialColor, t);
			}
			else
			{
				if(t.GetComponent<EnnemiePatrol>()!=null){
				EnnemieDeathController._instance.ActivateEnnemie(t.GetComponent<EnnemiePatrol>().MaterialColor, t);
				}
				else if(t.GetComponent<EnnemeieBonuse>()!=null){
				EnnemieDeathController._instance.ActivateEnnemie(t.GetComponent<EnnemeieBonuse>().MaterialColor, t);


				}
			}
		}
	}
	IEnumerator SlowTime()
    {
		/*Time.timeScale = 0.5f;
		for (int i = 0; i < 100; i++)
        {
			yield return new WaitForSeconds(0.02f);
			if (Time.timeScale >= 1)
			{
				Time.timeScale = 1;
			}
            else
            {
				Time.timeScale +=  Time.deltaTime*i/10;
			}*/
		yield return new WaitForSeconds(1f);
		stopKilling = false;


	}
		
		
		
	
	void GetSettingData(float speed, float ennemydetect, bool _autofocuse, bool _vibration)
	{

		AutoFocuse = _autofocuse;
		vibration = _vibration;
	}

	void VerifeLevel(bool b)
    {
		isBonuceLevel = b;
    }

	IEnumerator DesactivateEnnemy(GameObject obj)
    {
		yield return new WaitForSeconds(0.2f);
		obj.SetActive(false);
    }


	public GameObject LazerParticle;
	void DeathWithLaser()
    {
		LazerParticle.SetActive(true);
        if (EventController.gameLoose!=null)
        {
			EventController.gameLoose();

		}

	}
	void DeathWithSpike()
	{
		BloodObj.SetActive(true);

		StartCoroutine(ActivateBlood());
	}

	IEnumerator ActivateBlood()
    {
		yield return new WaitForSeconds(0.1f);
		if (EventController.gameLoose != null)
		{
			EventController.gameLoose();

		}
	}
	void GameWin()
    {
		transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);

	}
}


public enum WeopenType {none,Knife,Disc,Butcher }