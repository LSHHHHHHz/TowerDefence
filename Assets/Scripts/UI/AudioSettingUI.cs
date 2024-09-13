using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingUI : MonoBehaviour
{
    GameManager gameManager;
    AudioManager audioManager;
    Transform bgmPlayer;
    Transform sfxPlayer;
    public AudioSource bgmSource;
    public AudioSource[] sfxSource;
    private void Awake()
    {
        gameManager = GameManager.instance;
        audioManager = gameManager.audioManager;
    }
    private void Start()
    {
        for (int i = 0; i < gameManager.transform.childCount; i++)
        {
            Transform child = gameManager.transform.GetChild(i);

            if (child.name == "BgmPlayer")
            {
                bgmPlayer = child;
                bgmSource = bgmPlayer.GetComponent<AudioSource>(); 
            }

            else if (child.name == "SfxPlayer")
            {
                sfxPlayer = child;
                sfxSource = sfxPlayer.GetComponentsInChildren<AudioSource>();
            }
        }
        gameObject.SetActive(false);
    }
    public void SetBGMSourceVolume(float volume)
    {
        audioManager.bgmVolume = volume;
        bgmSource.volume = volume;
    }
    public void SetSFXMSourceVolume(float volume)
    {
        foreach (var sfx in sfxSource)
        {
            sfx.volume = volume;
        }

    }
    public void ExitAudioUI()
    {
        gameObject.SetActive(false);
    }
}
