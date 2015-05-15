using UnityEngine;
using System.Collections;


public class SpellFireball : Spell
{
	[System.Serializable]
	public class Loadout
	{
		public SpellType type;
		public float lifetime;
		public float damage;
		public float speed;
		public float acceleration;
		public float knockBack;
		
		public bool isExplosive;
		public float explodeRadius;
		public float explodeDamage;
	}
	public Loadout loadout;
	public GameObject fireballObject;
	private GameObject target;
	
	
	
	
	void Start()
	{
		if(fireballObject == null)
		{
			Debug.Log("[SpellFireball] Please assign a fireball Object to script");
		}
	}
	
	override public void Cast()
	{
		// instantiate fireball object
		GameObject go = GameObject.Instantiate (fireballObject, owner.spellSpawnTransform.position, owner.spellSpawnTransform.rotation) as GameObject;
		Fireball fb = go.GetComponent<Fireball>();
		if(fb)
		{
			GameObject target = PlayerManager.Target();
			if(target)
			{
				fb.target = target;
			}
			else
			{
				fb.target = EnemyManager.GetClosest(transform.position, PlayerManager.GetPlayerScript().maxTargetingRange);
			}
			
			fb.loadout = loadout;
		}
		else
		{
			Debug.Log("[SpellFireball] Could not find Fireball script in object");
		}
	}
	
	
	
	
	
}
