using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Characters.Scripts
{
    [RequireComponent(typeof(AnimationsSystem))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class HealthSystem : MonoBehaviour
    {
        #region Editor tweakable fields

        [SerializeField]
        private Health health;
        
        #endregion
        
        #region Private fields

        [NotNull]
        private AnimationsSystem animationsSystem;
        
        #endregion
        
        #region Unity callbacks

        private void Start()
        {
            animationsSystem = GetComponent<AnimationsSystem>();
        }

        #endregion
        
        #region Public methods
        
        public HealthState TakeDamage(float amount)
        {
            health.CurrentValue -= amount;

            if (health.CurrentValue <= 0)
            {
                animationsSystem.PlayDeathAnimation();
            }
            else
            {
                animationsSystem.PlayHitAnimation();
            }
            
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