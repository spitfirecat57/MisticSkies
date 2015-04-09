using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

	public GameObject starto;
	public GameObject silence;
	public GameObject sparkles;
	public GameObject crash;
	public GameObject disapear;

	void Start () {
		}

		void OnMouseDown()
		{
			if (Input.GetMouseButtonDown (0)) 
			{

				starto.animation.Play ("Camera_1");
			    audio.Play();
			    silence.SetActive(false);
				renderer.enabled = false;

			    sparkles.SetActive(true);
				crash.SetActive(true);
				disapear.SetActive(false);
			}
			
		}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
