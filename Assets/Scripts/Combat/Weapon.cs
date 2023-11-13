using RPG.Combat;
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon",menuName ="Weapon/newWeapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField]private AnimatorOverrideController weaponAnimatorOverrideController;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponRange;
    [SerializeField] private float percentageBonus;
    [SerializeField] private float weaponAttackCD;
    [SerializeField] private bool isRightHand;
    [SerializeField] private Projectile projectile;

    private const string weaponName = "Weapon";

    private Transform currentHand;
    public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {

        DestroyHandOldWeapon(rightHand,leftHand);
        if(weaponPrefab && weaponAnimatorOverrideController)
        {
            
            if(isRightHand)
            {
                currentHand = rightHand;
            }    
            else
            {
                currentHand = leftHand;
            }

            GameObject weaponInstance = Instantiate(weaponPrefab, currentHand);
            weaponInstance.name = weaponName;

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if(weaponAnimatorOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponAnimatorOverrideController;
            }
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            
        }
    }

    private void DestroyHandOldWeapon(Transform rightHand, Transform leftHand)
    {
        if(rightHand.Find(weaponName))
        {
            Destroy(rightHand.Find(weaponName).gameObject);
        }
        if (leftHand.Find(weaponName))
        {  
            Destroy(leftHand.Find(weaponName).gameObject);
        }
    }

    public void LaunchProjectile(GameObject attacker,Transform target,float damage)
    {
        Projectile projectileInstance = Instantiate(projectile, currentHand.position,Quaternion.identity);

        projectileInstance.SetTarget(attacker, target, damage);
    }

    public bool HasProjectile()
    {
        return projectile != null;
    }

    public float GetDamage()
    {
        return weaponDamage;
    }

    public float GetPercentageBonus()
    {
        return percentageBonus;
    }

    public float GetRange()
    {
        return weaponRange;
    }
    public float GetAttackCD()
    {
        return weaponAttackCD;
    }

}
