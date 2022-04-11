using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixer mixer;
	public const string MUSIC_KEY = "MusicVolume";
	public const string SFX_KEY = "SoundVolume";
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		else
		{
			Destroy(gameObject);
		}

		LoadVolume();
	}

	void LoadVolume() // Volume saved in VolumeSettings.cs
	{
		float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
		float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
		
		mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
		mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
	} 
}
