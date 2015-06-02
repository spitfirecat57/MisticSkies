using UnityEngine;
using System.Collections;

public class BackgroundMusicManager : MonoBehaviour
{
	private static BackgroundMusicManager instance;

	public float fadeTime;
	public SoundSource[] backgroundMusic;

	private SoundSource currentSong;
	private int currentSongIndex = 0;
	private float currentSongLength;
	private float currentTimeIntoSong;

	private static bool isPlaying = false;
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}

		isPlaying = false;
		
		if(backgroundMusic.Length > 0)
		{
			currentSong = backgroundMusic[0];
			currentSongIndex = 0;
			currentSongLength = currentSong.source.clip.length;
		}
		else
		{
			print ("[SoudnManager] There are no songs in the background music list");
		}
	}
	
	public static void StartPlaying()
	{
		print ("StartPlaying");
		isPlaying = true;
		instance.currentSong.source.Play ();
	}

	public static void StopPlaying()
	{
		print ("StopPlaying");
		isPlaying = false;
		instance.currentSong.source.Stop();
	}

	void Update()
	{
		if(isPlaying)
		{
			currentTimeIntoSong += Time.deltaTime;
			print ("CurrentTimeIntoSong = " + currentTimeIntoSong);
			print ("CurretnSongIndex = " + currentSongIndex);

			float startToFade = (currentSongLength - fadeTime);

			// fade out
			if(currentTimeIntoSong > startToFade)
			{
				currentSong.source.volume = (currentTimeIntoSong - startToFade) / (currentSongLength - startToFade);

				// next song
				if(currentTimeIntoSong >= currentSongLength)
				{
					NextSong();
				}
			}

			// fade in
			if(currentTimeIntoSong < fadeTime)
			{
				currentSong.source.volume = currentTimeIntoSong / fadeTime;
			}
		}

		if(Input.GetKeyDown(KeyCode.P))
		{
			if(!isPlaying)
			{
				isPlaying = true;
			}
			currentSong.source.time = currentSongLength - fadeTime - 2.0f;
			currentTimeIntoSong = currentSongLength - fadeTime - 2.0f;
		}
	}

	void NextSong()
	{
		currentSong.source.Stop ();
		currentSongIndex = (currentSongIndex + 1) % backgroundMusic.Length;
		currentSong = backgroundMusic [currentSongIndex];
		currentSong.source.time = 0.0f;
		currentSong.source.Play ();
		currentSongLength = currentSong.source.clip.length;
		currentTimeIntoSong = 0.0f;
	}
	
}
