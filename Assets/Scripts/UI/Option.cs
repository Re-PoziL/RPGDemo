using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private void Start()
    { 
        InputSystem.Instance.OnOptionAction += OnOptionAction;
        gameObject.SetActive(false);
    }

    private void OnOptionAction(object sender, System.EventArgs e)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
