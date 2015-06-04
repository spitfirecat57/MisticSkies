using UnityEngine;
using System.Collections;

public class MoveDirection : MonoBehaviour {

	public int Speed;
	
	// Use this for initialization
	void Start () 
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		//audio.Play(100);
	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.Translate(0, Time.deltaTime * Speed, 0);

	}
}
