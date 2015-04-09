using UnityEngine;
using System.Collections;

public class Continue_an : MonoBehaviour {

	public GameObject nex;
	public GameObject next;
	public GameObject nextt;
	public GameObject nexttt;
	public GameObject nextttt;


	void Start () {
		
	}

	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			next.SetActive(false);
			nextt.SetActive(false);
			nexttt.SetActive(false);
			nextttt.SetActive(true);
			//otherr.enabled = false;
			nextttt.animation.Play ("Continuee_1");
			audio.Play();
			
		}
		
	}
	
	
	
	
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		
	}
}
