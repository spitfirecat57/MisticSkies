using UnityEngine;
using System.Collections;

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

	public float maxHealth;
	private float currentHealth;
	public float walkSpeed;
	public float turnSpeedDegreesPerSecond;
	public float noticePlayerDist;

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
	public GameObject flameTowerIndicatorCircle;
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

	// Fireball
	public GameObject fireballPrefab;
	public float fireballDamage;
	public float fireballSpeed;

	//private bool noticedPlayer = false;
	private bool isCatastrophic = false;

	private NavMeshAgent navAgent;

	void Start()
	{
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.speed = walkSpeed;

		currentHealth = maxHealth;

		attackArray = new AttackDelegate[(int)BasicAttacks.COUNT];
		attackArray[(int)BasicAttacks.Headbutt] 	= Headbutt;
		attackArray[(int)BasicAttacks.FlameTower] 	= FlameTower;
		attackArray[(int)BasicAttacks.FlameThrower] = FlameThrower;
	}

	void Update()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition();


		transform.LookAt(new Vector3 (playerPos.x, transform.position.y, playerPos.z));


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

//		// Basic
//		if(!isCatastrophic)
//		{
//			basicAttackTimer += Time.deltaTime;
//			if(basicAttackTimer >= basicAttackCooldownTime)
//			{
//				int attack = Random.Range (0, (int)BasicAttacks.COUNT);
//				//attackArray[attack]();
//				print ("[fIREbOSS] aTTACKED");
//			}
//		}
//		// Catastrophic
//		else
//		{
//			cataAttackTimer += Time.deltaTime;
//			if(cataAttackTimer >= cataCooldownTime)
//			{
//				Fireball();
//			}
//		}

	}

	private void Headbutt()
	{
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

	private void FlameTower()
	{
		StartCoroutine ("FlameTowerCo");
	}
	private IEnumerator FlameTowerCo()
	{
		float flameIndicatorTimer = 0.0f;
		float flameTowerTimer = 0.0f;

		GameObject fti = GameObject.Instantiate (flameTowerIndicatorCircle, transform.position, transform.rotation) as GameObject;
		while(flameIndicatorTimer < flameTowerDuration)
		{
			flameIndicatorTimer += Time.deltaTime;
			fti.transform.localScale = new Vector3(fti.transform.localScale.x * flameTowerSpeed, fti.transform.localScale.y, fti.transform.localScale.z * flameTowerSpeed);
			yield return null;
		}
		Destroy(fti);

		GameObject ftf = GameObject.Instantiate (flameTowerIndicatorCircle, transform.position, transform.rotation) as GameObject;
		while(flameTowerTimer < flameTowerDuration)
		{
			flameTowerTimer += Time.deltaTime;
			ftf.transform.localScale = new Vector3(ftf.transform.localScale.x * flameTowerSpeed, ftf.transform.localScale.y, ftf.transform.localScale.z * flameTowerSpeed);
			yield return null;
		}
		Destroy(ftf);
	}

	private void FlameThrower()
	{
		StartCoroutine ("FlameThrowerCo");
	}
	private IEnumerator FlameThrowerCo()
	{
		flameThrowerPE.Play ();
		yield return null;
	}

	private void Fireball()
	{
	}
}
