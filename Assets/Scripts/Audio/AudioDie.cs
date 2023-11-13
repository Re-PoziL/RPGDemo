using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDie : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Health.OnDie += Health_OnDie;
    }

    private void Health_OnDie()
    {
        audioSource.Play();
    }

}
