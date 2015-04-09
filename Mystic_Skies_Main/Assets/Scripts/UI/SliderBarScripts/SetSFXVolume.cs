using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetSFXVolume : MonoBehaviour
{
	private Slider slider;
	
	void Start()
	{
		slider = GetComponent<Slider>();
	}
	
	void Update()
	{
		SoundManager.SetVolume (SoundManager.SoundType.SFX, slider.value);
	}
}
