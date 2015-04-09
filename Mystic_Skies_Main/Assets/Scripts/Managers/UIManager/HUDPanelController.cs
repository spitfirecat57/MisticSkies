using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDPanelController : PanelController
{
	// player icon
	public Image imagePlayerIcon;

	// health and mana bars
	public Slider sliderHealth;
	public Slider sliderMana;

	// spell icons
	public Image FireSpellIcon_1;
	public Image FireSpellIcon_2;
	public Image RockSpellIcon_1;
	public Image RockSpellIcon_2;
	public Image WaterSpellIcon_1;
	public Image WaterSpellIcon_2;
	// currently displayed spell icons
	private Image imageSpellFire;
	private Image imageSpellRock;
	private Image imageSpellWater;

	void Awake()
	{
		imageSpellFire  = FireSpellIcon_1;
		imageSpellRock  = RockSpellIcon_1;
		imageSpellWater = WaterSpellIcon_1;
	}
}
