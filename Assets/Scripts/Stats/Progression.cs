using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progress", menuName = "Progress/newProgress")]
    public class Progression : ScriptableObject
    {
        

        [System.Serializable]
        class ProgressionCharacterClass
        {
            
            public CharacterClass characterClass;
            public ProgressionStat[] progressionStat;
        }

        [System.Serializable]
        class ProgressionStat
        {
            //状态枚举
            public Stat stat;
            //每个状态的不同等级
            public float[] levels;
        
        }

        [SerializeField] private ProgressionCharacterClass[] progressionCharacterClasses;
        private Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable;



        public float GetStat(Stat stats, CharacterClass characterClass, int level)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stats];
            if (level > levels.Length)
            {
                return 0;
            }
            return levels[level - 1];
        }

        public int GetLevels(Stat stats, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookupTable[characterClass][stats];
            return levels.Length;
        }



        private void BuildLookup()
        {
            if (lookupTable != null)
                return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass item in progressionCharacterClasses)
            {
                Dictionary<Stat, float[]> stat = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in item.progressionStat)
                {
                    stat[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[item.characterClass] = stat;
            }
        }
    }
}