using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField]private GameObject foreGround;
    [SerializeField]private Health health;
    // Update is called once per frame
    void Update()
    {
        float healthFraction = health.GetHealthFraction();
        foreGround.transform.localScale = new Vector3(healthFraction, 1, 1);
    }
}
