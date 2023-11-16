using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarUI : MonoBehaviour
{
    public TextMeshProUGUI percent;

    private Scrollbar scrollbar;
    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.onValueChanged.AddListener(ChangePercent);
    }

    private void Start()
    {
        

    }

    private void ChangePercent(float value)
    {
        percent.text = String.Format("{0:0}%",value*100);
    }
}
