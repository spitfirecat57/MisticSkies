using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayPlayerMana : MonoBehaviour
{
	Slider slider;
	Text text;
	public SpellType type;
	
	void Start()
	{
		slider = GetComponent<Slider>();
		text = GetComponentInChildren<Text> ();
	}
	
	void Update()
	{
		float playerMana = 0.0f;
		float playerMaxMana = 0.0f;

		if(type == SpellType.Fire)
		{
			playerMana = PlayerManager.GetPlayerFireMana();
			playerMaxMana = PlayerManager.GetPlayerMaxFireMana();
		}
		else if(type == SpellType.Water)
		{
			playerMana = PlayerManager.GetPlayerWaterMana();
			playerMaxMana = PlayerManager.GetPlayerMaxWaterMana();
		}
		else
		{
			playerMana = PlayerManager.GetPlayerRockMana();
			playerMaxMana = PlayerManager.GetPlayerMaxRockMana();
		}
		 
		slider.value = (playerMana == 0.0f ? 0.0f : playerMana) / playerMaxMana;
		if(text)
		{
			text.text = (playerMana == 0.0f ? 0 : (int)playerMana) + " / " + playerMaxMana;
		}
	}
}
