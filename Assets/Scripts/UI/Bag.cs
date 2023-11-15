using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public CloseButton closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.OnCloseButtonDown += CloseButton_OnCloseButtonDown;
    }

    private void CloseButton_OnCloseButtonDown()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
