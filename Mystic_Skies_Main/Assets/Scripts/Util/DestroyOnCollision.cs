using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{
	public string[] collisionExceptions;

	void OnCollisionEnter(Collision collision)
	{
		if(collisionExceptions.Length > 0)
		{
			foreach(string c in collisionExceptions)
			{
				if(collision.collider.CompareTag(c))
				{
					return;
				}
			}
		}

		Destroy(gameObject);
	}
}
