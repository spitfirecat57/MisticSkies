using UnityEngine;
using System.Collections;

public class BackgroundMusicManager : MonoBehaviour
{
	private static BackgroundMusicManager instance;

	public float fadeTime;
	public AudioSource[] backgroundMusic;
	private static AudioSource[] backgroundMusicStatic;
	
	private static int currentSongIndex = 0;
	private static float currentSongLength;
	private static float currentTimeIntoSong;

	private static bool isPlaying = false;
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		isPlaying = false;

		currentSongIndex = 0;
		currentSongLength = (backgroundMusic.Length > 0) ? backgroundMusic[0].clip.length : 0.0f;

		backgroundMusicStatic = new AudioSource[backgroundMusic.Length];
		for(int i = 0; i < backgroundMusic.Length; ++i)
		{
			backgroundMusicStatic[i] = backgroundMusic[i];
			SoundSource ss = gameObject.AddComponent<SoundSource>();
			ss.source = backgroundMusic[i];
			ss.type = SoundManager.SoundType.Music;
		}

		if(backgroundMusic.Length > 0)
		{
			StartPlaying();
		}
	}
	
	public static void StartPlaying()
	{
		//print ("StartPlaying");
		isPlaying = true;
		backgroundMusicStatic[currentSongIndex].Play();
	}

	public static void StopPlaying()
	{
		//print ("StopPlaying");
		isPlaying = false;
		backgroundMusicStatic[currentSongIndex].Stop();
	}

	void Update()
	{
		if(isPlaying)
		{
			currentTimeIntoSong += Time.deltaTime;
			//print ("CurrentTimeIntoSong = " + currentTimeIntoSong);
			//print ("CurretnSongIndex = " + currentSongIndex);

			float startToFade = (currentSongLength - fadeTime);

			// fade out
			if(currentTimeIntoSong > startToFade)
			{
				backgroundMusicStatic[currentSongIndex].volume = (currentTimeIntoSong - startToFade) / (currentSongLength - startToFade);

				// next song
				if(currentTimeIntoSong >= currentSongLength)
				{
					NextSong();
				}
			}

			// fade in
			if(currentTimeIntoSong < fadeTime)
			{
				backgroundMusicStatic[currentSongIndex].volume = currentTimeIntoSong / fadeTime;
			}
		}

		if(Input.GetKeyDown(KeyCode.P))
		{
			if(!isPlaying)
			{
				isPlaying = true;
			}
			backgroundMusicStatic[currentSongIndex].time = currentSongLength - fadeTime - 2.0f;
			currentTimeIntoSong = currentSongLength - fadeTime - 2.0f;
		}
	}

	void NextSong()
	{
		backgroundMusicStatic[currentSongIndex].Stop ();
		currentSongIndex = (currentSongIndex + 1) % backgroundMusicStatic.Length;
		backgroundMusicStatic[currentSongIndex].time = 0.0f;
		backgroundMusicStatic[currentSongIndex].Play ();
		currentSongLength = backgroundMusicStatic[currentSongIndex].clip.length;
		currentTimeIntoSong = 0.0f;
	}
	
}
