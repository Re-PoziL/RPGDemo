using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Saving;
namespace RPG.SceneManagement
{
    enum DestinationIdentifier
    {
        A,
        B,
        C,
        D,
        E,
    }

    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;
        [SerializeField] private float fadeOutTime;
        [SerializeField] private float fadeInTime;
        [SerializeField] private float fadeWaitTime;
        [SerializeField] private Transform playerGenerationPoint;
        [SerializeField] private DestinationIdentifier destinationIdentifier;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }

        }

        private IEnumerator Transition()
        {
            if (sceneIndex == -1)
                yield break;
            DontDestroyOnLoad(gameObject);
            Fade fade = FindObjectOfType<Fade>();
            yield return fade.FadeOut(fadeOutTime);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneIndex);

            savingWrapper.Load();

            Portal portal = GetProtalInNewScene();
            UpdatePlayer(portal);
            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fade.FadeIn(fadeInTime);
            Destroy(gameObject);

        }

        private void UpdatePlayer(Portal portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().Warp(portal.playerGenerationPoint.position);
            player.transform.rotation = portal.playerGenerationPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetProtalInNewScene()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                    continue;
                if (portal.destinationIdentifier != this.destinationIdentifier)
                    continue;
                return portal;
            }
            return null;
        }
    }
}