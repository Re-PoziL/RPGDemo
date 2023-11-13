using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] private float gainExperience;

        public event Action OnGainExperience;

        public void GainExperience(float experience)
        {
            gainExperience += experience;
            OnGainExperience.Invoke();
        }

        public float GetExperience()
        {
            return gainExperience;
        }

        public object CaptureState()
        {
            return gainExperience;
        }

        public void RestoreState(object state)
        {
            gainExperience = (float)state;
        }
    }
}