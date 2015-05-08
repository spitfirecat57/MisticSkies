using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
	[HideInInspector]
	public SpellMeteor.Loadout loadout;
	[HideInInspector]
	public Vector3 targetPos;


	void Start()
	{
		if (targetPos != Vector3.zero)
		{
			rigidbody.velocity = (targetPos - transform.position).normalized * loadout.speed;
		}
		else
		{
			Vector3 newTargetPos = PlayerManager.GetPlayerPosition() + (PlayerManager.GetPlayerForward() * 10.0f);
			rigidbody.velocity = (newTargetPos - transform.position).normalized * loadout.speed;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Collider[] cols = Physics.OverlapSphere (transform.position, loadout.explodeRadius);
		foreach(Collider col in cols)
		{
			if(col.CompareTag("Enemy"))
			{
				Enemy enemy = col.GetComponent<Enemy>();
				if(enemy)
				{
					//Debug.Log("[Fireball] Explosion hit an enemy");
					enemy.TakeDamage(loadout.type, loadout.damage);
				}
			}
			else if(col.CompareTag("FireBoss"))
			{
				FireBoss enemy = col.GetComponent<FireBoss>();
				if(enemy)
				{
					//Debug.Log("[Fireball] Explosion hit an enemy");
					enemy.TakeDamage(loadout.type, loadout.damage);
				}
			}

		}
		Destroy (gameObject);
	}
}
