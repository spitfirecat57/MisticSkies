﻿using UnityEngine;
using System.Collections;

public class SpellRockNova : Spell
{
	[System.Serializable]
	public class Loadout
	{
		public float radius;
		public float maxHeight;
	}
	public Loadout loadout;
	public GameObject rockNovaObject;
	
	
	
	void Start()
	{
		if(rockNovaObject == null)
		{
			Debug.Log("[SpellRockNova] Please assign a rockNova Object to script");
		}
	}
	
	
	override public void Cast()
	{
		//Transform parentTransform = gameObject.GetComponentInParent<Transform> ();
		GameObject go = GameObject.Instantiate (rockNovaObject, transform.position, Quaternion.identity) as GameObject;
		RockNova rn = go.GetComponent<RockNova> ();
		
		if(rn)
		{
			rn.type = type;
			rn.damage = damage;
			rn.loadout = loadout;
		}
		else
		{
			Debug.Log("[SpellRockNova] Could not find RockNova script in object");
		}
	}
}
