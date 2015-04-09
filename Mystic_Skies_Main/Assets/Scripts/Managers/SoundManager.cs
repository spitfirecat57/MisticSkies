using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
	public enum SoundType
	{
		Music,
		SFX,
		Speech,
		COUNT
	}
	
	private static List<SoundSource>[] soundSources;
	private static float[] volumes;
	private static float masterVolume;

	private bool awakeHasBeenCalled = false;

	void Awake()
	{
		if(!awakeHasBeenCalled)
		{
			soundSources = new List<SoundSource>[(int)SoundType.COUNT];
			volumes = new float[(int)SoundType.COUNT];
			for(int i = 0; i < (int)SoundType.COUNT; ++i)
			{
				soundSources[i] = new List<SoundSource>();
				volumes[i] = 1.0f;
			}
			masterVolume = 1.0f;
		}
	}

	// Register/Remove audio sources
	public static void RegisterSoundSource(SoundSource source)
	{
		source.source.volume = source.MaxVolume() * volumes[(int)source.type];
		soundSources[(int)source.type].Add(source);
	}
	public static void RemoveSoundSource(SoundSource source)
	{
		soundSources [(int)source.type].Remove(source);
	}
	

	// Set audio source category voume
	public static void SetVolume(SoundType type, float volume)
	{
		// TODO; shouldn't need this because only options menu sliders can control overall sound
		if (volume < 0.0f)
		{
			volume = 0.0f;
		}

		volumes[(int)type] = volume;
		foreach(SoundSource source in soundSources[(int)type])
		{
			source.source.volume = source.MaxVolume() * volume;
		}
	}

	// set master voluume
	public static void SetMasterVolume(float volume)
	{
		masterVolume = volume;
		AudioListener.volume = volume;
	}



	void Update()
	{
		if(Input.GetKey(KeyCode.O))
		{
			//float vol = masterVolume;
			//float vol = volumes[(int)SoundType.Music];
			float vol = volumes[(int)SoundType.SFX];
			//float vol = volumes[(int)SoundType.Speech];

			vol -= 0.5f * Time.deltaTime;
			if(vol < 0.0f)
			{
				vol = 0.0f;
			}

			//SetMasterVolume(masterVolume);
			//SetVolume(SoundType.Music, vol);
			SetVolume(SoundType.SFX, vol);
			//SetVolume(SoundType.Speech, vol);

			print ("[SoundManager] setting volume to " + vol);
		}
		else if(Input.GetKey(KeyCode.P))
		{
			//float vol = masterVolume;
			//float vol = volumes[(int)SoundType.Music];
			float vol = volumes[(int)SoundType.SFX];
			//float vol = volumes[(int)SoundType.Speech];
			
			vol += 0.5f * Time.deltaTime;
			if(vol > 1.0f)
			{
				vol = 1.0f;
			}
			
			//SetMasterVolume(masterVolume);
			//SetVolume(SoundType.Music, vol);
			SetVolume(SoundType.SFX, vol);
			//SetVolume(SoundType.Speech, vol);

			print ("[SoundManager] setting volume to " + vol);
		}

		//print ("[SoundManager]\t MasterVolume\t" + masterVolume);
		//print ("[SoundManager]\t MusicVolume\t" + volumes[(int)SoundType.Music]);
		//print ("[SoundManager]\t SFXVolume\t" + volumes[(int)SoundType.SFX]);
		//print ("[SoundManager]\t SpeechVolume\t" + volumes[(int)SoundType.Speech]);
	}


}





