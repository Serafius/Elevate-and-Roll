using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    [Range(0f, 1f)] public float bgmVolume = 1f; // Current BGM volume
    [Range(0f, 1f)] public float sfxVolume = 1f; // Current SFX volume

    [SerializeField] private Slider bgmSlider; // Slider for BGM volume
    [SerializeField] private Slider sfxSlider; // Slider for SFX volume

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumeSettings();
    }

    private void Start()
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = bgmVolume;
            bgmSlider.onValueChanged.AddListener(UpdateBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
        }

        // Apply initial volumes
        UpdateBGMVolume(bgmVolume);
        UpdateSFXVolume(sfxVolume);
    }

    public void UpdateBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp(volume, 0f, 1f);

        if (AudioManager.instance != null)
        {
            foreach (AudioSource bgmSource in AudioManager.instance.BGM)
            {
                bgmSource.volume = bgmVolume;
            }
        }

        SaveVolumeSettings();
    }

    public void UpdateSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp(volume, 0f, 1f);

        if (AudioManager.instance != null)
        {
            foreach (AudioSource sfxSource in AudioManager.instance.SFX)
            {
                sfxSource.volume = sfxVolume;
            }
        }

        SaveVolumeSettings();
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f); 
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f); 
    }

    private void OnDestroy()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.RemoveListener(UpdateBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(UpdateSFXVolume);
        }
    }
}
