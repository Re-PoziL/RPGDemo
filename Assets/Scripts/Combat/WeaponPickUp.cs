using RPG.Attributes;
using RPG.Combat;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour,IRaycastable
{
    [SerializeField]private Weapon weapon;
    [SerializeField]private float respawnTime = 3f;
    [SerializeField] private float healValue = 0f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PickUp(other.GetComponent<Fighter>());
        }
    }
     
    private void PickUp(Fighter fighter)
    {
        if (weapon == null)
        { 
            fighter.GetComponent<Health>().Heal(healValue);
        }
        else
        {
            fighter.EquipWeapon(weapon);
        }
        StartCoroutine(HideForSeconds(respawnTime));
    }

    private IEnumerator HideForSeconds(float respawnTime)
    {
        ShowPickUp(false);
        yield return new WaitForSeconds(respawnTime);
        ShowPickUp(true);
    }

    private void ShowPickUp(bool shouldShow)
    {
        transform.GetComponent<Collider>().enabled = shouldShow;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldShow);
        }
    }

    public bool HandleRaycast(PlayerController playerController)
    {
        if(Input.GetMouseButton(0))
        {
            PickUp(playerController.GetComponent<Fighter>());
        }
        return true;
    }

    public CursorType GetCursorType()
    {
        return CursorType.PickUp;
    }
}
