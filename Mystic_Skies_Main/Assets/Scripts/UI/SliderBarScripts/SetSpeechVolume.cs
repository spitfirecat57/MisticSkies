using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetSpeechVolume : MonoBehaviour
{
	private Slider slider;
	
	void Start()
	{
		slider = GetComponent<Slider>();
	}
	
	void Update()
	{
		SoundManager.SetVolume (SoundManager.SoundType.Speech, slider.value);
	}
}
