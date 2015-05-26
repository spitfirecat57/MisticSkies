using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayPlayerMana : MonoBehaviour
{
	Slider slider;
	Text text;
	
	void Start()
	{
		slider = GetComponent<Slider>();
		text = GetComponentInChildren<Text> ();
	}
	
	void Update()
	{
		float playerMana = PlayerManager.GetPlayerMana();
		
		slider.value = (playerMana == 0.0f ? 0.0f : playerMana) / PlayerManager.GetPlayerMaxMana();
		text.text = (playerMana == 0.0f ? 0 : (int)playerMana) + " / " + (int)PlayerManager.GetPlayerMaxMana();
	}
}
