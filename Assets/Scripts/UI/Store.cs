using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    
    private void Start()
    {
        InputSystem.Instance.OnStoreAction += OnStoreAction;
        gameObject.SetActive(false);
    }

    private void OnStoreAction(object sender, System.EventArgs e)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
