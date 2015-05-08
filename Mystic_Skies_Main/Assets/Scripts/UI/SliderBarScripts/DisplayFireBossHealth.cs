using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayFireBossHealth : MonoBehaviour
{
	private Slider slider;
	public FireBoss fb;

	void Start()
	{
		slider = GetComponent<Slider>();
	}

	void Update()
	{
		slider.value = fb.currentHealth / fb.maxHealth;
	}
}
