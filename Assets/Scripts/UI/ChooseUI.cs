using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseUI : MonoBehaviour
{
    public Button useButton;
    public Button discardButton;

    private void Start()
    {
        useButton.onClick.AddListener(() =>{
            Debug.Log("use");  
        });

        discardButton.onClick.AddListener(() => {
            Debug.Log("discard");
        });
    }


}