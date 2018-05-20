using System.Diagnostics.CodeAnalysis;
using Game.Characters.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Characters.Player.Scripts
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(PlayerInputSystem))]
    [RequireComponent(typeof(WeaponSystem))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class PlayerActor : MonoBehaviour, IActor, IDamageable
    {
        #region Private fields
        
        [NotNull]
        [Inject]
        private QuestSystem questSystem;

        [NotNull]
        private HealthSystem healthSystem;

        [NotNull]
        private PlayerInputSystem inputSystem;

        [NotNull]
        private WeaponSystem weaponSystem;
        
        #endregion
        
        #region Unity callbacks

        private void Start()
        {
            healthSystem = GetComponent<HealthSystem>();
            inputSystem = GetComponent<PlayerInputSystem>();
            weaponSystem = GetComponent<WeaponSystem>();
        }

        #endregion
        
        public bool TakeDamage(float amount)
        {
            return healthSystem.TakeDamage(amount);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}