using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class StoreItem : MonoBehaviour
    {
        public Button button;
        public Image image;
        public GameObject choose;
        public TextMeshProUGUI numberText;
        public Item item;
        private void Start()
        {
            
            button.onClick.AddListener(() =>
            {
                choose.SetActive(!choose.activeSelf);
            });
            choose.SetActive(false);

            choose.GetComponent<StoreChoose>().OnBuy += BagItem_OnBuy;
        }

        private void BagItem_OnBuy()
        {
            StoreManager.Instance.Buy(item);
            
        }
    }
}