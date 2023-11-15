using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class BagItem : MonoBehaviour
    {
        public Button button;
        public Image image;
        public GameObject choose;
        public TextMeshProUGUI numberText;
        private void Start()
        {
            
            button.onClick.AddListener(() =>
            {
                choose.SetActive(!choose.activeSelf);
            });
            choose.SetActive(false);

        }

    }
}