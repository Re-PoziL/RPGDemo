using RPG.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject peristentObjectPrefab;
        private static bool hasSpawend = false;
        private void Awake()
        {
            if (hasSpawend)
                return;
            GameObject peristentObjcet = Instantiate(peristentObjectPrefab);
            DontDestroyOnLoad(peristentObjcet);
            hasSpawend = true;
        }
    }
}