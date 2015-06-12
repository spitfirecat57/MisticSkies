using UnityEngine;
using System.Collections;

public class WatterBridges : MonoBehaviour
{
	public GameObject clips;
	public GameObject DelCol;
	public ParticleSystem w;
	public string TriggerAttack;

	//public GameObject camClip;
//	public int cameraLife;
	// Use this for initialization
	void Start () 
	{
		clips.SetActive(false);
		DelCol.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider col)
	{
		print ("Collided with " + col.name);
		if(col.gameObject.name == TriggerAttack)
		{
			clips.SetActive(true);
			//camClip.SetActive(true);
			//Destroy(camClip, cameraLife);
			DelCol.SetActive(false);
			w.Play ();
		}
		
	}
}
