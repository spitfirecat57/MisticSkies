using UnityEngine;
using System.Collections;

public class BossAttack_anim : MonoBehaviour {
	
	public GameObject attack;
	// Use this for initialization
	void Start ()
	{
		attack.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.name == "Player")
		{
			attack.SetActive(true);
		}
	}

}
