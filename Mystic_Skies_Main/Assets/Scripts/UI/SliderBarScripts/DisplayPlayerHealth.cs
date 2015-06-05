using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayPlayerHealth : MonoBehaviour
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
		float playerHealth = PlayerManager.GetPlayerHealth ();

		slider.value = (playerHealth == 0.0f ? 0.0f : playerHealth) / PlayerManager.GetPlayerMaxHealth();
		if(text)
		{
			text.text = (playerHealth == 0.0f ? 0 : (int)playerHealth) + " / " + (int)PlayerManager.GetPlayerMaxHealth();
		}
	}
}
