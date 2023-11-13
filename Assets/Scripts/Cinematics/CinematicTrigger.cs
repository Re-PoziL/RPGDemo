using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{

    public class CinematicTrigger : MonoBehaviour
    {
        bool isTrigger = false;
        private void OnTriggerEnter(Collider other)
        {
            if(!isTrigger && other.CompareTag("Player"))
            {
                isTrigger = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}