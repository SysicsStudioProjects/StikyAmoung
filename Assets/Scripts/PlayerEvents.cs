using System.Collections;

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

	bool Istarting;

	bool isBonuceLevel;

	public GameObject BloodObj;

	public static WeopenType weopenType;
	WeopenType weopen;
	public float RangeWeopen;
	public LayerMask obstacleMask;
	public PlayerMovement playerMovement;
	// Start is called before the first frame update
	private void OnEnable()
	{
		if (EventController.setPlayer != null)
		{
			EventController.setPlayer(transform);
		}

		EventController.isBonuceLevel += VerifeLevel;


		EventController.startKillEvent += Kill;
		EventController.sendSettingData += GetSettingData;
		EventController.deathWithLaser += DeathWithLaser;
		EventController.deathWithSpike += DeathWithSpike;
		EventController.gameWin += GameWin;
		EventController.ennemieDown += EnnemieDown;
		EventController.gameStart += GameStart;
		InvokeRepeating("CaroutineTarget",0,0.2f);

		switch (weopenType)
		{
			case WeopenType.none:

				Skin s = new Skin();
				int index = Singleton._instance.skins.allSkins.FindIndex(d => d.name == "Knife1");
				s = Singleton._instance.skins.allSkins[index];
				s.state = SkinState.useIt;
				Singleton._instance.save();
				weopenType = WeopenType.Knife;
				EventController.useSkin(s);
				RangeWeopen = 2.5f;
				range = 4;
				break;
			case WeopenType.Knife:
				RangeWeopen = 2.5f;
				range = 4;
				break;
			case WeopenType.Disc:
				range = 8;
				RangeWeopen = 8;
				break;
			case WeopenType.Butcher:
				RangeWeopen = 2.5f;
				range = 4;

				break;
			case WeopenType.gogo:
				RangeWeopen = 8f;
				range = 8;

				break;
			case WeopenType.ironman:
				RangeWeopen = 8f;
				range = 8;

				break;
			default:
				break;
		}
		stopKilling = false;
		target = null;
		switchTarget = null;
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		EventController.isBonuceLevel -= VerifeLevel;

		EventController.startKillEvent -= Kill;
		EventController.sendSettingData -= GetSettingData;
		EventController.deathWithLaser -= DeathWithLaser;
		EventController.deathWithSpike -= DeathWithSpike;
		EventController.gameWin -= GameWin;
		EventController.ennemieDown -= EnnemieDown;
		EventController.gameStart -= GameStart;



	}
	private void Start()
	{
		switch (weopenType)
		{
			case WeopenType.none:
				Skin s = new Skin();
				int index = Singleton._instance.skins.allSkins.FindIndex(d => d.name == "Knife1");
				s = Singleton._instance.skins.allSkins[index];
				s.state = SkinState.useIt;
				Singleton._instance.save();
				EventController.useSkin(s);
				weopenType = WeopenType.Knife;
				RangeWeopen = 2.5f;
				range = 4;
				break;
			case WeopenType.Knife:
				RangeWeopen = 2.5f;
				range = 4;
				break;
			case WeopenType.Disc:
				range = 8;
				RangeWeopen = 8;
				break;
			case WeopenType.Butcher:
				RangeWeopen = 2.5f;
				range = 4;
				break;
			case WeopenType.gogo:
				RangeWeopen = 8f;
				range = 8;

				break;
			case WeopenType.ironman:
				RangeWeopen = 8f;
				range = 8;

				break;
			default:
				break;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{
        if (playerMovement.enabled==false)
        {
			target = null;
			switchTarget = null;
			stopKilling = false;
			return;
        }
        if (weopen!=weopenType)
        {
			switch (weopenType)
			{
				case WeopenType.none:
					RangeWeopen = 2.5f;
					
					range = 4;
					Skin s = new Skin();
					int index = Singleton._instance.skins.allSkins.FindIndex(d => d.name == "Knife1");
					s = Singleton._instance.skins.allSkins[index];
					s.state = SkinState.useIt;
					Singleton._instance.save();
					weopenType = WeopenType.Knife;
					EventController.useSkin(s);
					break;
				case WeopenType.Knife:
					RangeWeopen = 2.5f;
					range = 4;
					break;
				case WeopenType.Disc:
					range = 8;
					RangeWeopen = 8;
					break;
				case WeopenType.Butcher:
					RangeWeopen = 2.5f;
					range = 4;
					break;
				case WeopenType.gogo:
					RangeWeopen = 8f;
					range = 8;

					break;
				case WeopenType.ironman:
					RangeWeopen = 8f;
					range = 8;

					break;
				default:
					break;
			}
			weopen = weopenType;
		}
		if (target != null && AutoFocuse&&target.gameObject.activeInHierarchy)
		{
			LookTotarget(0.8f);


		}



	}

	void UpdateTarget()
	{
        if (playerMovement.enabled==false)
        {
			return;
        }
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

			if (EventController.canKill != null)
			{
				EventController.canKill(null, 0);
			}

		}
		if (stopKilling == false)
		{
			if (switchTarget != null)
			{
                if (RangeWeopen<4)
                {
					if (EventController.canKill != null)
					{
						EventController.canKill(target, shortestDistance);
					}
					//StartCoroutine(SlowTime());
				}
				


				if (shortestDistance <= RangeWeopen)
				{
					
					
					if (RangeWeopen > 4)
					{
						Vector3 dirToTarget = (target.position - transform.position).normalized;
						if (!Physics.Raycast(transform.position, dirToTarget, shortestDistance, obstacleMask))
						{
							Kill();
							StartCoroutine(SlowTime());
						}
                        else
                        {
							StartCoroutine(SlowTime());
						}
					}
                    else
                    {
						Kill();
					}
					stopKilling = true;
				}

				//target = null;
			}

			switchTarget = target;


		}

	}


	void CaroutineTarget()
	{
        
		
		/*if (canKill == false)
		{
			target = null;
			switchTarget = null;
			if (EventController.canKill != null)
			{
				EventController.canKill(null, 0);
			}
		}*/
		if (stopKilling == false&&Istarting)
		{
			UpdateTarget();
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

				StartCoroutine(SlowTime());

				break;
			case WeopenType.Butcher:

				anim.SetTrigger("attackButcher");
				
				break;
			case WeopenType.ironman:
				anim.SetTrigger("iron");
				break;

			case WeopenType.gogo:
				anim.SetTrigger("gogo");
				break;
			default:
				break;
		}
		



	}
	bool stopKilling;
	public void KillEvent(Transform t)
	{
		if (t != null)
		{
			target = t;
		}
		stopKilling = true;
		if (target != null)
		{
			switch (weopenType)
			{
				case WeopenType.none:
					StartCoroutine(DesactivateEnnemy(target.gameObject, 0.1f));
					break;
				case WeopenType.Knife:
					StartCoroutine(DesactivateEnnemy(target.gameObject, 0.1f));
					break;
				case WeopenType.Disc:
					StartCoroutine(DesactivateEnnemy(target.gameObject, 0.1f));
					break;
				case WeopenType.Butcher:
					StartCoroutine(DesactivateEnnemy(target.gameObject, 0.1f));
					break;
				case WeopenType.ironman:
					StartCoroutine(DesactivateEnnemy(target.gameObject, 0.1f));
					break;
				case WeopenType.gogo:
					StartCoroutine(DesactivateEnnemy(target.gameObject, 0.1f));
					break;
				default:
					break;
			}
			StartCoroutine(SlowTime());
			/*if (EventController.canKill != null)
			{
				EventController.canKill(null,0);
			}*/
			if (vibration)
			{
				//Handheld.Vibrate();
			}

			LookTotarget(1);
            
            
		//	target.gameObject.SetActive(false);

			




		}
		
        if (target!=null)
        {
			
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
		print("hey hey");
		yield return new WaitForSeconds(0.5f);
		stopKilling = false;
		target = null;
		switchTarget = null;
		//playerMovement.enabled = true;



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

	IEnumerator DesactivateEnnemy(GameObject obj,float t)
	{
        
		float a = 0;
        if (t>0.1f)
        {
			a = 0.3f;
        }
        else
        {
			a = 0.1f;
        }
		yield return new WaitForSeconds(a);
		
		
		yield return new WaitForSeconds(t-0.1f);
		if (isBonuceLevel)
		{
			EnnemieDeathController._instance.ActivateEnnemie(obj.GetComponent<EnnemeieBonuse>().MaterialColor, obj.transform);
		}
		else
		{
			if (obj.GetComponent<EnnemiePatrol>() != null)
			{
				EnnemieDeathController._instance.ActivateEnnemie(obj.GetComponent<EnnemiePatrol>().MaterialColor, obj.transform);
			}
			else if (obj.GetComponent<EnnemeieBonuse>() != null)
			{
				EnnemieDeathController._instance.ActivateEnnemie(obj.GetComponent<EnnemeieBonuse>().MaterialColor, obj.transform);


			}
		}
		obj.SetActive(false);
		if (weopenType != WeopenType.Disc)
		{
			//playerMovement.enabled = false;
		}
		else
		{
			playerMovement.enabled = true;

		}
	}


	public GameObject LazerParticle;
	void DeathWithLaser()
	{
		LazerParticle.SetActive(true);
		if (EventController.gameLoose != null)
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
	//bool canKill;
	/*void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.transform.tag != enemyTag && hit.transform.tag != "Ground")
		{
			canKill = false;

		}
		else
		{
			canKill = true;
		}
	}*/

	void EnnemieDown(EnnemiePatrol obj)
    {
		target = null;
		switchTarget = null;
		stopKilling = false;

	}
	void GameStart(bool b)
    {
		Istarting = b;
    }
}

public enum WeopenType {none,Knife,Disc,Butcher,ironman,gogo }