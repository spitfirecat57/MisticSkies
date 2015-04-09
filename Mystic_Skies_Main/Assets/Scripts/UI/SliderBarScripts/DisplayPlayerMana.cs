using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayPlayerMana : MonoBehaviour
{
	Slider slider;
	
	void Start()
	{
		slider = GetComponent<Slider>();
	}
	
	void Update()
	{
		slider.maxValue = PlayerManager.GetPlayerMaxMana();
		slider.value = PlayerManager.GetPlayerMana();
	}
}
