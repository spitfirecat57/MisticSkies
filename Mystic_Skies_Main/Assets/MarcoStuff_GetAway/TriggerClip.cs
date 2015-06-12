using UnityEngine;
using System.Collections;

public class TriggerClip : MonoBehaviour
{

	public GameObject clip;
	public string TriggerAttack;
	public float destroytime;

	void Update()
	{
		clip.SetActive (false);
		//Destroy(clip, destroytime);
	}

	void OnTriggerEnter (Collider col)
	{
		print ("Collided with " + col.name);
		if(col.gameObject.name == TriggerAttack)
		{
			clip.SetActive(true);
			Destroy(clip, destroytime);
		}
		
	}
}
