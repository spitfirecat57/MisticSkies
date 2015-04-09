using UnityEngine;
using System.Collections;

public class save : MonoBehaviour {

	public GameObject othe;
	public GameObject other;
	public GameObject otherr;
	public GameObject otherrr;
	public GameObject otherrrr;


	void Start () {

	}



	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			otherrrr.SetActive(false);
			otherrr.SetActive(false);
			otherr.SetActive(false);
			other.SetActive(true);
			//otherr.enabled = false;
			other.animation.Play ("Camera_2");
			audio.Play();
		}

		}
	

	// Update is called once per frame
	void Update () {

	}
}
