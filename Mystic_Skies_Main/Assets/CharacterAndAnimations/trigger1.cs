using UnityEngine;
using System.Collections;

public class trigger1 : MonoBehaviour {

	public GameObject clip1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	



	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Player")
		{
			clip1.SetActive(true);
			Destroy (gameObject);
			Destroy(clip1, 6);
		}
	}
}
