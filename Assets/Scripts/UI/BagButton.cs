using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagButton : MonoBehaviour
{
    public GameObject Bag;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Bag.SetActive(!Bag.activeSelf);
        });
        Bag.SetActive(false);
    }
}
