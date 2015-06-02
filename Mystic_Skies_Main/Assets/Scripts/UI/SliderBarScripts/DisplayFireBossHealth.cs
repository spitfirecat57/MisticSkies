using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class DisplayFireBossHealth : MonoBehaviour
{
	private static DisplayFireBossHealth instance = null;

	private Slider slider = null;
	private static FireBoss fb = null;

	void Start()
	{
		slider = GetComponent<Slider>();
		gameObject.SetActive (false);

		if(instance == null)
		{
			instance = this;
		}
	}

	public static void Activate(FireBoss boss)
	{
		fb = boss;
		instance.gameObject.SetActive (true);
	}

	void Update()
	{
		if(fb)
		{
			slider.value = fb.currentHealth / fb.maxHealth;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}
