using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagChoose : MonoBehaviour
{
    public Button useButton;
    public Button discardButton;
    public TextMeshProUGUI numberText;

    private int number;

    private void Awake()
    {
    }
    private void Start()
    {
        number = int.Parse(numberText.text);
        useButton.onClick.AddListener(() =>{
            Debug.Log("use");
            number--;
            numberText.text = number.ToString();
            if(number == 0)
            {
                transform.GetComponentInParent<BagItem>().gameObject.SetActive(false);
                
            }
        });

        discardButton.onClick.AddListener(() => {
            Debug.Log("discard");
            number--;
            numberText.text = number.ToString();
            if (number == 0)
            {
                transform.GetComponentInParent<BagItem>().gameObject.SetActive(false);
            }
        });
        
    }

}
