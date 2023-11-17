using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    
    private void Start()
    {
        InputSystem.Instance.OnBagAction += OnBagAction;
        gameObject.SetActive(false);
    }

    private void OnBagAction(object sender, System.EventArgs e)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
