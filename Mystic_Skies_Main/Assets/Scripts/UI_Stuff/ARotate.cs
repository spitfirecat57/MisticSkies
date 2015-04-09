using UnityEngine;
using System.Collections;

public class ARotate : MonoBehaviour {

	public float RotSpeed;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, Time.deltaTime * RotSpeed, 0));
	}
}
