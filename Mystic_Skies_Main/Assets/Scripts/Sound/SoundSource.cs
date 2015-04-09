using UnityEngine;
using System.Collections;

public class SoundSource : MonoBehaviour
{
	public SoundManager.SoundType type;
	public AudioSource source;
	private float maxVolume;

	void Start()
	{
		if(source != null)
		{
			maxVolume = source.volume;
			SoundManager.RegisterSoundSource(this);
		}
		else
		{
			Debug.Log("[SoundSource] source is null!!!");
		}
	}

	void OnDestroy()
	{
		SoundManager.RemoveSoundSource(this);
	}

	public float MaxVolume()
	{
		return maxVolume;
	}
}
