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

    private void Awake()
    {
        UpdateText();
    }

    private void Start()
    {

        storeButton.onClick.AddListener(() =>
        {
            RebindButton(Binding.Store);
        });
        bagButton.onClick.AddListener(() =>
        {
            RebindButton(Binding.Bag);
        });
        optionButton.onClick.AddListener(() =>
        {
            RebindButton(Binding.Option);
        });

    }

    void UpdateText()
    {
        storeText.text = InputSystem.Instance.GetBinding(Binding.Store);
        bagText.text = InputSystem.Instance.GetBinding(Binding.Bag);
        optionText.text = InputSystem.Instance.GetBinding(Binding.Option);
    }

    private void ShowpressToBinding()
    {
        pressToBinding.gameObject.SetActive(true);
    }


    private void HidepressToBinding()
    {
        pressToBinding.gameObject.SetActive(false);
    }

    private void RebindButton(Binding binding)
    {
        ShowpressToBinding();
        InputSystem.Instance.ReBinding(binding, () =>
         {
             UpdateText();
             HidepressToBinding();
         });
    }

}
