using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip clip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
            return;
        if(!audioSource.isPlaying)
        {
            Recyle();
        }
    }
    
    private void Recyle()
    {
        Pool.Instance.Recycle(gameObject);
    }

    public void CreateAudio(AudioClip audioClip,float volume = 1)
    {
        this.clip = audioClip;
        audioSource.clip = clip;
        audioSource.volume = volume * AudioManager.Instance.GetAudioVolume();
        audioSource.Play();
    }

}
