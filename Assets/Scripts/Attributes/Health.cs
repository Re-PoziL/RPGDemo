using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {

        private float health = -1;

        private const string DIE = "die";
        private bool isDie = false;

        private LazyValue<float> maxHealth;

        public static event Action<float,GameObject> OnTakeDamage;

        public static event UnityAction OnDie;

        private void Awake()
        {
            maxHealth = new LazyValue<float>(GetInitialHealth);
        }

        private void Start()
        {
            if(health == -1)
            {
                health = maxHealth.value;
            }

            GetComponent<BaseStats>().OnLevelUp += ResetHealth;
        }


        private void OnDisable()
        {
            GetComponent<BaseStats>().OnLevelUp -= ResetHealth;
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void ResetHealth()
        {
            maxHealth.value = GetComponent<BaseStats>().GetStat(Stat.Health);
            health += maxHealth.value * (float)0.3;
            if(health > maxHealth.value)
            {
                health = maxHealth.value;
            }
        }

        public float GetHealthPercent()
        {
            return 100 * (health / maxHealth.value);
        }

        public float GetHealthFraction()
        {
            return health / maxHealth.value;
        }

        public float GetCurrentHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return maxHealth.value;
        }

        public bool IsDie()
        {
            return isDie;
        }

        public void TakeDamage(GameObject attacker, float damage)
        {
            OnTakeDamage?.Invoke(damage,this.gameObject);
            health = Mathf.Max(health - damage, 0);
            if(health == 0)
            {
                Die();
                if (attacker.CompareTag("Player"))
                {
                    AwardExperience(attacker);
                }
            }
        }

        public void Heal(float healValue)
        {
            health = Mathf.Min(GetMaxHealth(), GetCurrentHealth() + healValue);

        }

        private void AwardExperience(GameObject attacker)
        {
            if (attacker == null)
                return;
            Experience experience = attacker.GetComponent<Experience>();
            experience.GainExperience(gameObject.GetComponent<BaseStats>().GetStat(Stats.Stat.ExperienceReward));
                
        }

        public void Die()
        {
            isDie = true;
            OnDie?.Invoke();
            GetComponent<Animator>().SetTrigger(DIE);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        //获取状态
        public object CaptureState()
        {
            return health;
        }

        //重置状态
        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0)
            {
                Die();
            }
        }
    }
}