using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public AudioSource[] BGM => bgm; // Public property for accessing bgm array
    public AudioSource[] SFX => sfx; // Public property for accessing sfx array

    public bool playBgm;
    private int bgmIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Destroy duplicate AudioManager
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persist AudioManager across scenes
    }

    private void Update()
    {
        if (playBgm)
        {
            StopAllBGM();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    public void PlaySFX(int _sfxIndex)
    {
        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].pitch = Random.Range(0.9f, 1.1f);
            sfx[_sfxIndex].Play();
        }
    }

    public void PlaySFXOnce(int _sfxIndex)
    {
        if (_sfxIndex < sfx.Length)
        {
            if (!sfx[_sfxIndex].isPlaying)
            {
                sfx[_sfxIndex].pitch = Random.Range(0.9f, 1.1f);
                sfx[_sfxIndex].Play();
            }
        }
    }

    public void StopSFX(int _sfxIndex)
    {
        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].Stop();
        }
    }

    public void PlayRandomBgm()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;
        StopAllBGM();

        bgm[bgmIndex].Play();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
