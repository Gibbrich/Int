using System;
using System.Diagnostics.CodeAnalysis;
using Game.Characters.Scripts;
using Game.Scripts.Quests;
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

        [NotNull]
        [Inject]
        private UIController uiController;

        #endregion

        #region Unity callbacks

        private void Start()
        {
            healthSystem = GetComponent<HealthSystem>();
            inputSystem = GetComponent<PlayerInputSystem>();
            weaponSystem = GetComponent<WeaponSystem>();

            HealthState healthState = healthSystem.GetCurrentHealthState();
            uiController.UpdatePlayerHealthBarValues(healthState.CurrentHealth, healthState.MaxHealth);
        }

        #endregion

        #region Public methods

        public HealthState TakeDamage(float amount)
        {
            HealthState healthState = healthSystem.TakeDamage(amount);
            uiController.UpdateTargetHealthBarValues(healthState.CurrentHealth, healthState.MaxHealth);
            return healthState;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void SetTarget(IDamageable target)
        {
            weaponSystem.SetTarget(target);
        }

        public void OnInteraction(IBaseInteractable interactable)
        {
            if (interactable is IInteractable<object, AbstractQuest>)
            {
                inputSystem.OnInteraction();
            }
            else if (interactable is IInteractable<object, WeaponConfig>)
            {
                var weaponPickUp = interactable as IInteractable<object, WeaponConfig>;
                weaponSystem.PutWeaponInHand(weaponPickUp.Interact());
            }
            
        }

        #endregion
    }
}