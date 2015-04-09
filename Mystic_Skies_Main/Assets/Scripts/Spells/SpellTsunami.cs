using UnityEngine;
using System.Collections;

public class SpellTsunami : Spell
{
	[System.Serializable]
	public class Loadout
	{
		public SpellType type;
		public float damage;
		public float speed;
		public float range;
	}
	public Loadout loadout;
	public GameObject tsunamiObject;


	void Start()
	{
		if(tsunamiObject == null)
		{
			Debug.Log("[SpellTsunami] tsunamiObject reference is null");
		}
	}
	
	override public void Cast()
	{
		Vector3 launchPos = owner.spellSpawnTransform.position;
		Quaternion launchDir = owner.spellSpawnTransform.rotation;

		GameObject tsunami = GameObject.Instantiate (tsunamiObject, launchPos, launchDir) as GameObject;
		if(tsunami)
		{
			Tsunami tsu = tsunami.GetComponent<Tsunami>();
			if(tsu)
			{
				tsu.loadout = loadout;
			}
		}
	}
}
