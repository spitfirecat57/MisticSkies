using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour
{
	private static Singleton<T> instance = null;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
}
