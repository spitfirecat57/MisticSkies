using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public enum SoundType
	{
		Music,
		SFX,
		Speech,
		COUNT
	}
	protected static SoundManager instance = null;	
	
	private static List<SoundSource>[] soundSources;
	private static float[] volumes;
	private static float masterVolume;
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
		{
			Destroy(gameObject);
			return;
		}
		
		
		soundSources = new List<SoundSource>[(int)SoundType.COUNT];
		for(int i = 0; i < (int)SoundType.COUNT; ++i)
		{
			soundSources[i] = new List<SoundSource>();
		}
		volumes = new float[(int)SoundType.COUNT];
		// load saved values from playerprefs
		masterVolume                   = PlayerPrefs.GetFloat ("MasterVolume", 1.0f);
		volumes[(int)SoundType.Music]  = PlayerPrefs.GetFloat ("MusicVolume", 1.0f);
		volumes[(int)SoundType.SFX]    = PlayerPrefs.GetFloat ("SFXVolume", 1.0f);
		volumes[(int)SoundType.Speech] = PlayerPrefs.GetFloat ("SpeechVolume", 1.0f);
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
	
	
	// Set audio source category volume
	public static void SetVolume(SoundType type, float volume)
	{
		volume = Mathf.Clamp01 (volume);
		
		volumes[(int)type] = volume;
		
		print ("[SoundManager] soundSources.length = " + soundSources.Length);
		
		foreach(SoundSource source in soundSources[(int)type])
		{
			source.source.volume = source.MaxVolume() * volume;
		}
	}
	
	// set master volume
	public static void SetMasterVolumeStatic(float volume)
	{
		volume = Mathf.Clamp01 (volume);
		masterVolume = volume;
		AudioListener.volume = volume;
	}
	
	// Get audio source category volume
	public static float GetVolume(SoundType type)
	{
		return volumes[(int)type];
	}
	
	// Get master volume
	public static float GetMasterVolume()
	{
		return masterVolume;
	}
	
	
	//	// set master volume
	//	public void SetMasterVolume(float volume)
	//	{
	//		volume = Mathf.Clamp01 (volume);
	//		masterVolume = volume;
	//		AudioListener.volume = volume;
	//	}
	//	// set music volume
	//	public void SetMusicVolume(float volume)
	//	{
	//		volume = Mathf.Clamp01 (volume);
	//		volumes[(int)SoundType.Music] = volume;
	//	}
	//	// set SFX volume
	//	public void SetSFXVolume(float volume)
	//	{
	//		volume = Mathf.Clamp01 (volume);
	//		volumes[(int)SoundType.SFX] = volume;
	//	}
	//	// set speech volume
	//	public void SetSpeechVolume(float volume)
	//	{
	//		volume = Mathf.Clamp01 (volume);
	//		volumes[(int)SoundType.Speech] = volume;
	//	}
	
	public void SaveSoundOptions()
	{
		PlayerPrefs.SetFloat ("MasterVolume", masterVolume);
		PlayerPrefs.SetFloat ("MusicVolume",  volumes[(int)SoundType.Music]);
		PlayerPrefs.SetFloat ("SFXVolume",    volumes[(int)SoundType.SFX]);
		PlayerPrefs.SetFloat ("SpeechVolume", volumes[(int)SoundType.Speech]);
		PlayerPrefs.Save();
	}
	
	
	
}





