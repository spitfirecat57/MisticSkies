using UnityEngine;
using System.Collections;

public class InteractablePlaySound : Interactable
{
	public AudioSource sound;

	public override void OnEnter()
	{
	}

	public override void OnInteraction()
	{
		sound.Play();
	}

	public override void OnExit()
	{
	}
}
