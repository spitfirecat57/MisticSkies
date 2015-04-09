using UnityEngine;
using System.Collections;

public class back : MonoBehaviour {
	public GameObject oothe;
	public GameObject oother;
	public GameObject ootherr;
	public GameObject ootherrr;
	public GameObject ootherrrr;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			ootherrrr.SetActive(false);
			oother.SetActive(false);
			ootherr.SetActive(false);
			ootherrr.SetActive(true);
			//otherr.enabled = false;
			ootherrr.animation.Play ("Camera_3");
			audio.Play();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
