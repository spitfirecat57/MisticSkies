using UnityEngine;
using System.Collections;

public class InteractableToggleSound : Interactable
{
	public AudioSource sound;

	public override void OnEnter()
	{
	}

	public override void OnInteraction()
	{
		if(sound.isPlaying)
		{
			sound.Pause();
		}
		else
		{
			sound.Play();
		}
	}

	public override void OnExit()
	{
	}
}
