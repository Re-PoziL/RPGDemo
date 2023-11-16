using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //OnCloseButtonDown?.Invoke();
            transform.parent.gameObject.SetActive(false);
        });
    }
}
