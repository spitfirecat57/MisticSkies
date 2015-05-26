using UnityEngine;
using System.Collections;

public class WatterBridges : MonoBehaviour
{
	public GameObject clips;
	public GameObject DelCol;
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
		if(col.gameObject.name == "WaterWhip(Clone)")
		{
			clips.SetActive(true);
			DelCol.SetActive(false);
		}
		
	}
}
