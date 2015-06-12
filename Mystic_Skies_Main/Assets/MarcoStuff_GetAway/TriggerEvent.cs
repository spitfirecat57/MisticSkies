using UnityEngine;
using System.Collections;

public class TriggerEvent : MonoBehaviour 
{

	public GameObject clipObj;
	public GameObject clipObj1;

	public string collideWith;



	// Use this for initialization
	void Start ()
	{

		clipObj.SetActive(false);
		clipObj1.SetActive(false);

	}
	


	void OnTriggerEnter (Collider col)
	{
		print ("Collided with " + col.name);
		if(col.gameObject.name == collideWith)
		{
			clipObj.SetActive(true);
			clipObj1.SetActive(true);
			//clipObj2.SetActive(true);
			//ColGone.SetActive(false);
			//ColNew.SetActive(true);



		}
		
	}

}
