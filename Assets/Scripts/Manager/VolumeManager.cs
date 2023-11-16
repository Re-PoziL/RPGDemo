using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager:SingletonMono<VolumeManager>
{
    private float golbalVolume;
    public Scrollbar scrollbar;

    private string volumeFile = "Volume";
    private void Awake()
    {
        golbalVolume = SaveCommonData.LoadFloat(volumeFile);
        scrollbar.value = golbalVolume;
    }

    private void Start()
    {
        
        scrollbar.onValueChanged.AddListener((float volume) =>
        {
            golbalVolume = volume;
        });
        scrollbar.onValueChanged.Invoke(golbalVolume);
    }

    private void OnDestroy()
    {
        SaveCommonData.Save(volumeFile, golbalVolume);
    }

    public float GetGlobalVolume()
    {
        return golbalVolume;
    }

}
