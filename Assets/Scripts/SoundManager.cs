﻿using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	public AudioSource efxSource;                   
	public AudioSource musicSource;
	public AudioClip[] musicClips;                 
	public static SoundManager instance = null;     
	public float lowPitchRange = .95f;              
	public float highPitchRange = 1.05f;            
	
	
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
			
		DontDestroyOnLoad (gameObject);
	}
	
	public void PlaySingle(AudioClip clip)
	{
		efxSource.clip = clip;
		
		efxSource.Play ();
	}
	
	
	public void RandomizeSfx (params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);
		
		efxSource.pitch = randomPitch;
		
		efxSource.clip = clips[randomIndex];
		
		efxSource.Play();
	}

	public void ChangeMusic (int musicId) {
		musicSource.Stop();
		musicSource.clip = musicClips[musicId];
		musicSource.Play();
	}

	public bool IsPlayingFx () {
		return efxSource.isPlaying;
	}
}
