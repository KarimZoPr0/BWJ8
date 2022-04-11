using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SoundVolume";


    private void OnDisable()
    {
	    PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
	    PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    private void Awake()
    {
       musicSlider.onValueChanged.AddListener(DOSetMusicVolume);
       sfxSlider.onValueChanged.AddListener(DOSetSfxVolume);
    }


    private void Start()
    {
	    musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
	    sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    private void DOSetMusicVolume(float value)
    {
	    mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    private void DOSetSfxVolume(float value)
    {
	    mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
