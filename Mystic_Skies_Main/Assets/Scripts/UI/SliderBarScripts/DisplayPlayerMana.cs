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
		slider.value = PlayerManager.GetPlayerMana() / PlayerManager.GetPlayerMaxMana();
	}
}
