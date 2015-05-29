using UnityEngine;
using System.Collections;



public class pLAYtEST : MonoBehaviour {

	public GameObject Show;

	// Use this for initialization
	void Start () 
	{
		Show.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}




		void OnTriggerEnter(Collider other)
		{
		Show.SetActive(true);
		}

}
