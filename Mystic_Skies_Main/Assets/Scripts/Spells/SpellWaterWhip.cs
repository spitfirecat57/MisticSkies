using UnityEngine;
using System.Collections;

public class SpellWaterWhip : Spell
{
	[System.Serializable]
	public class Loadout
	{
		public float range;
		public float radius;
		public float minWidth;
		public float verticalSize;
	}
	public Loadout loadout;
	public GameObject waterWhipObject;



	void Start()
	{
		if(waterWhipObject == null)
		{
			Debug.Log("[SpellWaterWhip] Please assign a waterWhip Object to script");
		}
	}

	override public void Cast()
	{
		Transform parentTransform = gameObject.GetComponentInParent<Transform> ();
		GameObject go = GameObject.Instantiate (waterWhipObject, parentTransform.position + (parentTransform.forward * loadout.range * 0.5f), parentTransform.rotation) as GameObject;
		WaterWhip ww = go.GetComponent<WaterWhip> ();

		if(ww)
		{
			ww.type = type;
			ww.damage = damage;
			ww.loadout = loadout;
		}
		else
		{
			Debug.Log("[SpellWaterWhip] Could not find WaterWhip script in object");
		}

		Destroy(go, 0.15f);
	}	
}
