using UnityEngine;
using System.Collections;

public class BossAttack_anim : MonoBehaviour {

	public GameObject clip;
	public GameObject attack;
	public int cameraLife;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.name == "Player")
		{

			clip.SetActive(true);
			//Destroy(clip, 6);
			attack.SetActive(true);

			Destroy(clip, cameraLife);
		}
	}

}
