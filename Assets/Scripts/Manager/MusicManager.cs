using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : SingletonMono<MusicManager>
{
    private float musicVolume;
    public Scrollbar scrollbar;
    private string musicFile = "Music";
    private void Awake()
    {
        musicVolume = SaveCommonData.LoadFloat(musicFile);
    }
    private void Start()
    {
        scrollbar.value = musicVolume;
        scrollbar.onValueChanged.AddListener((float volume) =>
        {
            this.musicVolume = volume;
        });
        scrollbar.onValueChanged.Invoke(musicVolume);
    }

    private void OnDestroy()
    {
        SaveCommonData.Save(musicFile, musicVolume);
    }
    public float GetMusicVolume()
    {
        return musicVolume * VolumeManager.Instance.GetGlobalVolume();
    }
}
