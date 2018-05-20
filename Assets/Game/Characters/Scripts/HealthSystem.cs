using System;
using UnityEngine;
using Zenject;

namespace Game.Characters.Scripts
{
    public class HealthSystem : MonoBehaviour, IHealthSystem
    {
        #region Editor tweakable fields

        [SerializeField]
        private Health health;
        
        #endregion
        
        #region Private fields

        private IHealthSystemController healthSystemController;
        
        #endregion
        
        #region Public methods

        public bool TakeDamage(float amount)
        {
            return healthSystemController.TakeDamage(amount);
        }

        #endregion
        
        #region Private methods
        
        [Inject]
        private void Init(IHealthSystemController healthSystemController)
        {
            this.healthSystemController = healthSystemController;
            healthSystemController.Init(this, health);
        }        
        
        #endregion
    }

    public interface IHealthSystem : IBaseSystem
    {
        bool TakeDamage(float amount);
    }
}