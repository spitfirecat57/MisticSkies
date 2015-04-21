using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetMasterVolume : MonoBehaviour
{
	private Slider slider;

	void Start()
	{
		slider = GetComponent<Slider>();
	}

	void Update()
	{
		SoundManager.SetMasterVolumeStatic (slider.value);
	}
}
