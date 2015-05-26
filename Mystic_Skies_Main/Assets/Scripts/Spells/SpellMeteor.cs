using UnityEngine;
using System.Collections;

public class SpellMeteor : Spell
{
	[System.Serializable]
	public class Loadout
	{
		public float explodeRadius;
		public float speed;
		public float launchOffsetHeight;
	}
	public Loadout loadout;
	public GameObject meteorObject;


	void Start()
	{
		if(meteorObject == null)
		{
			Debug.Log("[SpellMeteor] meteorObject reference is null");
		}
	}

	public override void Cast()
	{
		Vector3 playerPos = PlayerManager.GetPlayerPosition();
		Vector3 launchPos = playerPos + (Vector3.up * loadout.launchOffsetHeight);
		GameObject meteor = GameObject.Instantiate (meteorObject, launchPos, Quaternion.identity) as GameObject;
		if(meteor)
		{
			Meteor met = meteor.GetComponent<Meteor>();
			if(met)
			{
				GameObject target = PlayerManager.Target();
				if(target)
				{
					met.targetPos = target.transform.position;
				}
				else
				{
					target = EnemyManager.GetClosest(transform.position, PlayerManager.GetPlayerScript().maxTargetingRange);
					if(target)
					{
						met.targetPos = target.transform.position;
					}
				}
				met.type = type;
				met.damage = damage;
				met.loadout = loadout;
			}
		}
	}
}
