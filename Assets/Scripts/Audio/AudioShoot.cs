using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShoot : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Projectile.OnShoot += Projectile_OnShoot;
    }

    private void OnDestroy()
    {
        Projectile.OnShoot -= Projectile_OnShoot;
    }

    private void Projectile_OnShoot()
    {
        Debug.Log(audioSource);
        audioSource.Play();
    }
}
