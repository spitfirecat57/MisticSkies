using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayPlayerHealth : MonoBehaviour
{
	Slider slider;

	void Start()
	{
		slider = GetComponent<Slider>();
	}

	void Update()
	{
		slider.maxValue = PlayerManager.GetPlayerMaxHealth();
		slider.value = PlayerManager.GetPlayerHealth ();
	}
}
