using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,60)]
        [SerializeField] private int level;

        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression;
        [SerializeField] private GameObject levelUpFX;
        [SerializeField] bool isUseModifier = false;

        private Experience experience;

        public event Action OnLevelUp;
        private void Awake()
        {
            experience = GetComponent<Experience>();
            
        }
        private void Start()
        {
            if (!experience)
                return;
            UpdateLevel();
        }

        private void OnEnable()
        {
            if (experience)
            {
                experience.OnGainExperience += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if(experience)
            {
                experience.OnGainExperience -= UpdateLevel;
            } 
        }

        private void UpdateLevel()
        {
            int newLevel = GetLevel();
            if(level == newLevel)
                return;
            if (level != newLevel)
            {
                level = newLevel;
                ShowLevelUpFX();
                OnLevelUp?.Invoke();
            }
        }

        private void ShowLevelUpFX()
        {
            if (levelUpFX == null)
                return;
            Instantiate(levelUpFX,transform);
        }

        public float GetStat(Stat currentStat)
        {
            return (GetBaseStat(currentStat) + GetAddModifiers(currentStat)) * GetPercentageModifers(currentStat);
        }

        private float GetBaseStat(Stat currentStat)
        {
            return progression.GetStat(currentStat, characterClass, level);
        }


        //获得自身身上的额外加成(武器伤害,buff等)
        private float GetAddModifiers(Stat currentStat)
        {
            
            float total = 0;
            foreach (IModifierProvider iModifierProvider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in iModifierProvider.GetAdditiveModifiers(currentStat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifers(Stat currentStat)
        {
            if (!isUseModifier)
                return 1;
            float total = 0;
            foreach (IModifierProvider iModifierProvider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in iModifierProvider.GetPercentageModifiers(currentStat))
                {
                    total += modifier;
                }
            }
            return 1+(total/100);
        }


        public int GetLevel()
        {
            float currentEXP = experience.GetExperience();
            int plevels = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int i = 1; i <= plevels; i++)
            {
                float xpToLevel = progression.GetStat(Stat.ExperienceToLevelUp,characterClass, i);
                if (currentEXP < xpToLevel)
                    return i;
            }
            return plevels + 1;
        }
    }
}