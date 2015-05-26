using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightTrigger : MonoBehaviour
{
	private static int currentTorchIndex = 0;
	private static List<LightTrigger> torches = new List<LightTrigger>();
	private static float timer = 0.0f;
	private static float sDelay = 2.0f;

	private Light mLight;
	private ParticleSystem mParticleSystem;

	public float delay;

	public int torchIndex;
	private static bool isActivated = false;


	void Start ()
	{
		mLight = GetComponentInChildren<Light>();
		mLight.enabled = false;
		mParticleSystem = GetComponentInChildren<ParticleSystem>();
		mParticleSystem.Stop ();

		torches.Add (this);

		if(torchIndex == 0)
		{
			print ("Torch 0 set sDelay to " + delay);
			sDelay = delay;
		}

		if(torchIndex == currentTorchIndex)
		{
			print ("Torch 0 set setactive(true)");
			Activate();
		}
	}

	void OnTriggerEnter (Collider col)
	{
		print ("Collided with " + col.name);
		if(col.gameObject.name == "Fireball(Clone)")
		{
			isActivated = true;
		}
	}

	void Update()
	{
		if(isActivated)
		{
			if(torchIndex == 0) timer += Time.deltaTime;

			if(timer >= sDelay)
			{
				foreach(LightTrigger lt in torches)
				{
					if(lt.torchIndex == currentTorchIndex)
					{
						lt.Activate();
					}
				}
				timer = 0.0f;
				++currentTorchIndex;
			}

			if(currentTorchIndex > torches.Count)
			{
				isActivated = false;
			}
		}
	}

	public void Activate()
	{
		mLight.enabled = true;
		mParticleSystem.Play ();
	}
}
