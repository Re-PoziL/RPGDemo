using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string SAVEDATA = "save";
        private float fadeTime = 0.2f;

        public static SavingWrapper Instance;

        public void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            
            Fade fade = FindObjectOfType<Fade>();
            fade.FadeOutImmediate();
            
            yield return GetComponent<SavingSystem>().LoadLastScene(SAVEDATA);
            yield return fade.FadeIn(fadeTime);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(SAVEDATA);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(SAVEDATA);
        }
    }
}