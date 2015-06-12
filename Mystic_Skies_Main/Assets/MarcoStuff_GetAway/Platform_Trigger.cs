using UnityEngine;
using System.Collections;

public class Platform_Trigger : MonoBehaviour 
{
	public GameObject platform;
	public string TriggerAttack;
	public float destroytime;

	// Use this for initialization
	void Start ()
	{
		platform.SetActive(false);
	}

	void OnTriggerEnter (Collider col)
	{
		print ("Collided with " + col.name);
		if(col.gameObject.name == TriggerAttack)
		{
			platform.SetActive(true);
			Destroy(platform, destroytime);
		}
		
	}
}
