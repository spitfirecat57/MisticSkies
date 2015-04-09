using UnityEngine;
using System.Collections;

public class Aura : MonoBehaviour {

	


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Tab)) {

			particleSystem.Play();
			audio.Play();

		}

	
	}
}
