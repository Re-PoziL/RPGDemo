using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Button storeButton;
    public Button bagButton;
    public Button optionButton;


    public TextMeshProUGUI storeText;
    public TextMeshProUGUI bagText;
    public TextMeshProUGUI optionText;

    public Image pressToBinding;


    private void Start()
    {
        storeButton.onClick.AddListener(() =>
        {
            ShowpressToBinding();
        });
        bagButton.onClick.AddListener(() =>
        {
            ShowpressToBinding();
        });
        optionButton.onClick.AddListener(() =>
        {
            ShowpressToBinding();
        });
    }

    private void ShowpressToBinding()
    {
        pressToBinding.gameObject.SetActive(true);
    }


    private void HidepressToBinding()
    {
        pressToBinding.gameObject.SetActive(true);
    }
}
