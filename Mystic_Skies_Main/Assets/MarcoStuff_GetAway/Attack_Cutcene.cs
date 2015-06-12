using UnityEngine;
using System.Collections;

public class Attack_Cutcene : MonoBehaviour {

	public string trigObj;
	public GameObject clip;
	

	// Use this for initialization
	void Start ()
	{
		clip.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		//print ("Collided with " + col.name);
		if(col.gameObject.name == trigObj)
		{
			clip.SetActive(true);
			//Destroy(clip, destroytime);
		}
		
	}
}
