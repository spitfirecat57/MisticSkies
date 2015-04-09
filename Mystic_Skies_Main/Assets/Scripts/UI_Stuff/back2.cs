using UnityEngine;
using System.Collections;

public class back2 : MonoBehaviour {
	public GameObject ooothe;
	public GameObject ooother;
	public GameObject oootherr;
	public GameObject oootherrr;
	public GameObject oootherrrr;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			ooothe.SetActive(false);
			ooother.SetActive(false);
			oootherr.SetActive(false);
			oootherrr.SetActive(false);
			oootherrrr.SetActive(true);
			//otherr.enabled = false;
			oootherrrr.animation.Play ("back");
			audio.Play();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
