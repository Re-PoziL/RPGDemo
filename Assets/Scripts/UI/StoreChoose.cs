using RPG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreChoose : MonoBehaviour
{
    public Button buyButton;
    public Button cancelButton;
    public TextMeshProUGUI numberText;

    private int number;

    public event Action OnBuy;

    private void Awake()
    {
    }
    private void Start()
    {
        number = int.Parse(numberText.text);
        buyButton.onClick.AddListener(() =>{
            Debug.Log("buy");
            number--;
            numberText.text = number.ToString();
            OnBuy?.Invoke();
            if (number == 0)
            {
                transform.GetComponentInParent<StoreItem>().gameObject.SetActive(false);
            }
        });

        cancelButton.onClick.AddListener(() =>
        {
            Debug.Log("cancel");
            gameObject.SetActive(false);
        });
        
    }

}
