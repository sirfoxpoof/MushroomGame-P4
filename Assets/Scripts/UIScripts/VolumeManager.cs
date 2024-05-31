using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolume, musicVolume, enemyVolume, sfxVolume;

    public void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            audioMixer.SetFloat("EnemyVolume", PlayerPrefs.GetFloat("enemyVolume"));
            audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));

            masterVolume.value = PlayerPrefs.GetFloat("MasterVolume");
            musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
            enemyVolume.value = PlayerPrefs.GetFloat("enemyVolume");
            sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        else
        {
            SetSliders();
        }
    }

    void SetSliders()
    {
        masterVolume.value = 0.5f;
        musicVolume.value = 0.5f;
        enemyVolume.value = 0.5f;
        sfxVolume.value = 0.5f;
    }


    public void SetVolumeMaster(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);

    }

    public void SetVolumeMusic(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
    }

    public void SetVolumeEnemy(float sliderValue)
    {
        audioMixer.SetFloat("enemyVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("enemyVolume", sfxVolume.value);
    }

    public void SetVolumeSFX(float sliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
    }
}
