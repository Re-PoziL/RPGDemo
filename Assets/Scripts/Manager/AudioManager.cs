using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : SingletonMono<AudioManager>
{
    private float audioVolume;
    public Scrollbar scrollbar;
    private string audioFile = "Audio";
    private void Awake()
    {
        audioVolume = SaveCommonData.LoadFloat(audioFile);   
    }
    private void Start()
    {
        scrollbar.value = audioVolume;
        scrollbar.onValueChanged.AddListener((float volume) =>
        {
            this.audioVolume = volume;
        });
        scrollbar.onValueChanged.Invoke(audioVolume);
    }

    private void OnDestroy()
    {
        SaveCommonData.Save(audioFile, audioVolume);
    }

    public float GetAudioVolume()
    {
        return audioVolume * VolumeManager.Instance.GetGlobalVolume();
    }
}
