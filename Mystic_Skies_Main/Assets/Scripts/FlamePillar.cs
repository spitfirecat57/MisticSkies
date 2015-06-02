using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlamePillar : MonoBehaviour
{
	private static List<FlamePillar> instances = null;

	void Start()
	{
		if(instances == null)
		{
			instances = new List<FlamePillar>();
		}

		instances.Add(this);
	}

	void Update()
	{
	}
}
