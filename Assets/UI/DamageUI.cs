using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageTextTemplate;
    // Start is called before the first frame update
    void Start()
    {
        Health.OnTakeDamage += Health_OnTakeDamage;
    }

    private void Health_OnTakeDamage(float damage,GameObject beAttacker)
    {
        TextMeshProUGUI damageText =  Instantiate(damageTextTemplate, beAttacker.transform.Find("DamageUI").transform);
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true);
    }



        
}
