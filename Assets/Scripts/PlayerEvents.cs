using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{

	public string enemyTag;
	public float range;
	public Transform target;
	
	public Animator anim;
	public GameObject DeathPlayer;
	public bool AutoFocuse;
	public bool vibration;

	
    // Start is called before the first frame update
    private void OnEnable()
    {
		EventController.startKillEvent += Kill;
		EventController.sendSettingData += GetSettingData;
	}

    private void OnDisable()
    {
		EventController.startKillEvent -= Kill;
		EventController.sendSettingData -= GetSettingData;

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
			if (EventController.canKill != null)
			{
				EventController.canKill(target);
			}
		}
        
	}

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
		anim.SetTrigger("attack");
		

    }
	bool stopKilling;
	public void KillEvent()
    {
        if (target!=null)
        {
			stopKilling = true;
			StartCoroutine(SlowTime());
			if (EventController.canKill != null)
			{
				EventController.canKill(null);
			}
			if (vibration)
            {
				Handheld.Vibrate();
			}
			
			LookTotarget(1);

			Destroy(target.gameObject, 0.2f);
            
			GameObject obj = Instantiate(DeathPlayer, target.position, transform.rotation);
			Destroy(obj, 2);
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
}
