using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
	public float delay = 5.0f;

	void Start()
	{
		Destroy (gameObject, delay);
	}
}
