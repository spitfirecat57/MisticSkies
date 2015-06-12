using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPlayerNumPotions : MonoBehaviour
{
	public ItemManager.PotionType potion_Type;

	private Text text;

	void Start()
	{
		text = GetComponent<Text> ();
	}

	void Update()
	{
		text.text = PlayerManager.GetPlayerScript ().potions [(int)potion_Type].value.ToString();
	}
}
