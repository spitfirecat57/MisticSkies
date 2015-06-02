using UnityEngine;
using System.Collections;

public class TriggerEvent : MonoBehaviour 
{

	public GameObject clipObj;
	public GameObject clipObj1;



	// Use this for initialization
	void Start ()
	{

		clipObj.SetActive(false);
		clipObj1.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider col)
	{
		print ("Collided with " + col.name);
		if(col.gameObject.name == "Player")
		{
			clipObj.SetActive(true);
			clipObj1.SetActive(true);
			//clipObj2.SetActive(true);
			//ColGone.SetActive(false);
			//ColNew.SetActive(true);



		}
		
	}

}
