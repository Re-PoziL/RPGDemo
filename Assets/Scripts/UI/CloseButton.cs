using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public event Action OnCloseButtonDown;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            OnCloseButtonDown?.Invoke();
        });
    }
}
