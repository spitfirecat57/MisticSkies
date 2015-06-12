using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]

public class FireBoss : MonoBehaviour
{
	private enum BasicAttacks
	{
		Headbutt,
		FlameTower,
		FlameThrower,
		COUNT
	}
	private delegate void AttackDelegate();
	private AttackDelegate[] attackArray;

	private SpellType mType = SpellType.Fire;
	public float maxHealth;
	public float currentHealth;
	public float knockBackPower;
	public float walkSpeed;
	public float turnSpeedDegreesPerSecond;
	public float noticePlayerDist;
	private bool lookAtPlayer = true;
	public Transform mouthPosition;
	private bool isInvincible = false;
	private bool hasBeenCatastrophic = false;

	// ===== Basic Attacks =====
	public float basicAttackCooldownTime;
	private float basicAttackTimer = 0.0f;

	// Headbutt
	public float headbuttDamage;
	//public float headbuttRange;
	public float headbuttTime;
	public float headbuttSpeed;
	public float headbuttRearingTime;
	public float headbuttRearingSpeed;

	// Flame Tower
	public GameObject flameTowerPrefab;
	public float flameTowerDamage;
	public float flameTowerSpeed;
	public float flameTowerDuration;
	public float flameTowerDelay;

	// Flame Thrower
	public float flameThrowerDamage;
	public float flameThrowerRange;
	public float flameThrowerResidueRadius;
	public ParticleSystem flameThrowerPE;


	// ===== Catastrophic Combustion Attacks =====
	public float cataCooldownTime;
	private float cataAttackTimer = 0.0f;
	public Transform cataPosition;
	public Transform[] flamePillarPositions;
	public GameObject flamePillarPrefab;
	private List<GameObject> flamePillars;
	public float flamePillarRiseSpeed;

	// Fireball
	public GameObject fireballPrefab;
	public float fireballDamage;
	public float fireballSpeed;

	//private bool noticedPlayer = false;
	private bool isCatastrophic = false;

	private Animator animator;


	private NavMeshAgent navAgent;

	void Start()
	{
		animator = GetComponentInChildren<Animator> ();

		navAgent = GetComponent<NavMeshAgent>();
		navAgent.speed = walkSpeed;

		currentHealth = maxHealth;

		attackArray = new AttackDelegate[(int)BasicAttacks.COUNT];
		attackArray[(int)BasicAttacks.Headbutt] 	= Headbutt;
		attackArray[(int)BasicAttacks.FlameTower] 	= FlameTower;
		attackArray[(int)BasicAttacks.FlameThrower] = FlameThrower;

		flamePillars = new List<GameObject>();

		EnemyManager.RegisterEnemy (gameObject);
	}

	void Update()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition();

		//---------------MARCO WAS HERE

		if(lookAtPlayer)
		//{
			//_________________________________________________________________________________________

			transform.forward = Vector3.RotateTowards(transform.forward, (playerPos - transform.position), turnSpeedDegreesPerSecond * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
		//	if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("rotate"))
		//	{
		//		animator.SetTrigger("rotate");
		//	}
			//_________________________________________________________________________________________

		//}
		//---------------MARCO WAS HERE


		if(Input.GetKeyDown(KeyCode.B))
		{
			Headbutt();
		}
		if(Input.GetKeyDown(KeyCode.N))
		{
			FlameTower();
		}
		if(Input.GetKeyDown(KeyCode.M))
		{
			FlameThrower();
		}
		if(Input.GetKeyDown(KeyCode.V))
		{
			Fireball();
		}

		if((transform.position - PlayerManager.GetPlayerPosition()).sqrMagnitude < noticePlayerDist * noticePlayerDist)
		{
			if(noticePlayerDist < 10000.0f)
			{
				noticePlayerDist = 10000.0f;
				DisplayFireBossHealth.Activate(this);
			}

			// Basic
			if(!isCatastrophic)
			{
				basicAttackTimer += Time.deltaTime;
				if(basicAttackTimer >= basicAttackCooldownTime)
				{
					int attack = Random.Range (0, (int)BasicAttacks.COUNT);
					attackArray[attack]();
					basicAttackTimer = 0.0f;
				}
			}
			// Catastrophic
			else
			{
				cataAttackTimer += Time.deltaTime;
				if(cataAttackTimer >= cataCooldownTime)
				{
					Fireball();
					cataAttackTimer = 0.0f;
				}

				flamePillars.RemoveAll(obj => obj == null);

				if(flamePillars.Count == 0)
				{
					isInvincible = false;
					isCatastrophic = false;
				}
			}
		}
	}

	private void Headbutt()
	{
		//MARCO WAS HERE _________________________________________________________________________________________
		if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("tackle"))
		{
			animator.SetTrigger("tackle");
		}
		//MARCO WAS HERE_________________________________________________________________________________________

		StartCoroutine ("HeadbuttCo");
	}
	private IEnumerator HeadbuttCo()
	{
		//lookAtPlayer = false;
		float headbuttTimer = 0.0f;
		navAgent.velocity = transform.forward * -headbuttRearingSpeed;
		while(headbuttTimer < headbuttRearingTime)
		{
			headbuttTimer += Time.deltaTime;
			yield return null;
		}
		// headbutt
		navAgent.velocity = transform.forward * headbuttSpeed;
		headbuttTimer = 0.0f;
		while(headbuttTimer < headbuttTime)
		{
			headbuttTimer += Time.deltaTime;
			yield return null;
		}
		navAgent.velocity = Vector3.zero;
		//lookAtPlayer = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			navAgent.velocity = Vector3.zero;
			PlayerManager.GetPlayerScript().TakeDamage(headbuttDamage);
			PlayerManager.GetPlayerScript().KnockBack(transform.forward * knockBackPower);
			Vector3 playervel = PlayerManager.GetPlayerObject().rigidbody.velocity;
			PlayerManager.GetPlayerObject().rigidbody.velocity = new Vector3(playervel.x, 5.0f, playervel.z);
			StopCoroutine("HeadbuttCo");
		}
	}

	private void FlameTower()
	{

		//Marco was here_________________________________________________________________________________________
		if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("firep"))
		{
			animator.SetTrigger("firePillar");
		}
		//Marco was here_________________________________________________________________________________________


		GameObject ft = GameObject.Instantiate (flameTowerPrefab, transform.position, transform.rotation) as GameObject;
		FlameTower ftscript = ft.GetComponent<FlameTower> ();
		ftscript.Init (flameTowerDamage, flameTowerSpeed, flameTowerSpeed, flameTowerDuration);

		//StartCoroutine ("FlameTowerCo");
	}


//	private IEnumerator FlameTowerCo()
//	{
//		float flameIndicatorTimer = 0.0f;
//		float flameTowerTimer = 0.0f;
//
//		GameObject fti = GameObject.Instantiate (flameTowerIndicatorCircle, transform.position, transform.rotation) as GameObject;
//		while(flameIndicatorTimer < flameTowerDuration)
//		{
//			flameIndicatorTimer += Time.deltaTime;
//			fti.transform.localScale = new Vector3(fti.transform.localScale.x * flameTowerSpeed, fti.transform.localScale.y, fti.transform.localScale.z * flameTowerSpeed);
//			yield return null;
//		}
//		Destroy(fti);
//
//		GameObject ftf = GameObject.Instantiate (flameTowerIndicatorCircle, transform.position, transform.rotation) as GameObject;
//		while(flameTowerTimer < flameTowerDuration)
//		{
//			flameTowerTimer += Time.deltaTime;
//			ftf.transform.localScale = new Vector3(ftf.transform.localScale.x * flameTowerSpeed, ftf.transform.localScale.y, ftf.transform.localScale.z * flameTowerSpeed);
//			yield return null;
//		}
//		Destroy(ftf);
//	}

	private void FlameThrower()
	{
		StartCoroutine ("FlameThrowerCo");
		//_________________________________________________________________________________________

		if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("fireB"))
		{
			animator.SetTrigger("FireBreath");
		}
		//_________________________________________________________________________________________

	}
	private IEnumerator FlameThrowerCo()
	{
		flameThrowerPE.Play ();
		yield return null;
	}

	private void Fireball()
	{

		//MARCO WAS HERE//_________________________________________________________________________________________

		if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("fireB"))
		{
			animator.SetTrigger("FireBreath");
		}
		//MARCO WAS HERE//_________________________________________________________________________________________

		GameObject.Instantiate (fireballPrefab, mouthPosition.position, transform.rotation);
	}

	public void TakeDamage(SpellType type, float damage)
	{
		float totalDamage = damage * Spell.GetDamageMultiplier (type, mType);
		if(!isInvincible)
		{
			currentHealth -= totalDamage;
		}

		if(currentHealth < maxHealth * 0.25f && !hasBeenCatastrophic)
		{
			currentHealth = maxHealth * 0.25f;
			StartCoroutine("InitCatastrophicCombustion");
		}

		if(currentHealth < 0.0f)
		{
			currentHealth = 0.0f;
			Destroy(gameObject);
		}
	}

	private IEnumerator InitCatastrophicCombustion()
	{
		// TODO: fix the pillar rise

		isCatastrophic = true;
		hasBeenCatastrophic = true;
		isInvincible = true;
		lookAtPlayer = false;
		navAgent.stoppingDistance = 0.0f;
		navAgent.SetDestination (cataPosition.position);

		int numFlamePillars = flamePillarPositions.Length;
		for(int i = 0; i < numFlamePillars; ++i)
		{
			GameObject fp = GameObject.Instantiate(flamePillarPrefab, flamePillarPositions[i].position - (Vector3.up * 5.0f), Quaternion.identity) as GameObject;
			fp.rigidbody.velocity = Vector3.up * flamePillarRiseSpeed;
			fp.collider.enabled = false;
			flamePillars.Add(fp);
		}

		float timeToRise = 10.0f / flamePillarRiseSpeed;
		float riseTimer = 0.0f;

		while(riseTimer < timeToRise)
		{
			riseTimer += Time.deltaTime;
			yield return null;
		}

		foreach(GameObject g in flamePillars)
		{
			g.rigidbody.velocity = Vector3.zero;
			g.collider.enabled = true;
		}

		lookAtPlayer = true;
	}





}






