using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDamage : MonoBehaviour
{

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Health.OnTakeDamage += Health_OnTakeDamage;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= Health_OnTakeDamage;
    }

    private void Health_OnTakeDamage(float arg1, GameObject beAttacker)
    {
        audioSource.Play();
    }
}
