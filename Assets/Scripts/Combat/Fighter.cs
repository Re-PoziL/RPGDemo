using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction,IModifierProvider
    {
        [SerializeField] private Transform rightHandTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private string defaultWeaponName = "Unarmed";
        private Animator animator;

        private const string ATTACK = "attack";
        private const string STOPATTACK = "stopAttack";
        private float timeSinceLastAttack = 0f;
        private Transform target;
        private Weapon currentWeapon = null;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            
        }

        //切换场景也会重新加载
        private void Start()
        {
            if (currentWeapon == null)
            {
                Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
                EquipWeapon(weapon);
            }
        }

        

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null)
                return;
            if (target.GetComponent<Health>().IsDie())
                return;
            //到攻击范围的位置
            if(!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {

            currentWeapon = weapon;
            currentWeapon?.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Transform GetTarget()
        {
            return target;
        }


        //animation event
        private void Hit()
        {
            if (target == null)
                return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(gameObject,target, damage);
            }
            else
            {
                Debug.Log(damage);
                target.GetComponent<Health>().TakeDamage(gameObject, damage);
            }
        }


        //animation event
        private void Shoot()
        {
            Debug.Log("Shoot");
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= currentWeapon.GetRange();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.position);
            if (target == null)
                return;
            if (timeSinceLastAttack >= currentWeapon?.GetAttackCD() && !target.GetComponent<Health>().IsDie())
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger(STOPATTACK);
            GetComponent<Animator>().SetTrigger(ATTACK);
        }

        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
            
        }

        public void Cancel()
        {
            GetComponent<Mover>().Cancel();
            StopAttack();
            target = null; 
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger(ATTACK);
            GetComponent<Animator>().SetTrigger(STOPATTACK);
        }


        //返回武器本身的伤害
        public IEnumerable<float> GetAdditiveModifiers(Stat currentStat)
        {
            if(currentStat == Stat.Damage)
            {
                yield return currentWeapon.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat currentStat)
        {
            if (currentStat == Stat.Damage)
            {
                yield return currentWeapon.GetPercentageBonus();
            }
        }
    }
}