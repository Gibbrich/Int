using System;
using UnityEngine;
using Zenject;

namespace Game.Characters.Scripts
{
    public class HealthSystem : MonoBehaviour
    {
        #region Editor tweakable fields

        [SerializeField]
        private Health health;
        
        #endregion
        
        #region Public methods
        
        public HealthState TakeDamage(float amount)
        {
            health.CurrentValue -= amount;
            return GetCurrentHealthState();
        }

        public HealthState GetCurrentHealthState()
        {
            return new HealthState(health.CurrentValue, health.MaxValue);
        }

        #endregion
    }

    public struct HealthState
    {
        #region Private fields

        private readonly float currentHealth;
        private readonly float maxHealth;

        #endregion
        
        #region Properties
        
        public float CurrentHealth
        {
            get { return currentHealth; }
        }

        public float MaxHealth
        {
            get { return maxHealth; }
        }        
        
        #endregion

        #region Public methods

        public HealthState(float currentHealth, float maxHealth)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
        }

        #endregion
    }
}