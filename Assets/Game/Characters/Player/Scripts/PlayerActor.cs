using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Game.Characters.Scripts;
using Game.Scripts.Quests;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Characters.Player.Scripts
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(PlayerInputSystem))]
    [RequireComponent(typeof(WeaponSystem))]
    [RequireComponent(typeof(AnimationsSystem))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class PlayerActor : MonoBehaviour, IActor, IDamageable
    {
        private const string PLAYER_LOAD_POINT = "PlayerLoadPoint";
        
        #region Private fields

        [NotNull]
        private QuestSystem questSystem;

        [NotNull]
        private HealthSystem healthSystem;

        [NotNull]
        private PlayerInputSystem inputSystem;

        [NotNull]
        private AnimationsSystem animationsSystem;

        [NotNull]
        private WeaponSystem weaponSystem;

        [NotNull]
        private UIController uiController;

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            healthSystem = GetComponent<HealthSystem>();
            inputSystem = GetComponent<PlayerInputSystem>();
            weaponSystem = GetComponent<WeaponSystem>();
            animationsSystem = GetComponent<AnimationsSystem>();
            questSystem = GetComponent<QuestSystem>();

            InitOuterDependencies();
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoad;
        }

        #endregion

        #region Public methods

        public HealthState TakeDamage(float amount)
        {
            HealthState healthState = healthSystem.TakeDamage(amount);
            if (healthState.CurrentHealth <= 0)
            {
                animationsSystem.PlayDeathAnimation();
            }
            else
            {
                animationsSystem.PlayHitAnimation();
            }
            uiController.UpdatePlayerHealthBarValues(healthState.CurrentHealth, healthState.MaxHealth);
            return healthState;
        }

        public HealthState GetCurrentHealthState()
        {
            return healthSystem.GetCurrentHealthState();
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
                animationsSystem.PlayInteractAnimation();
            }
            else if (interactable is IInteractable<object, WeaponConfig>)
            {
                var weaponPickUp = (IInteractable<object, WeaponConfig>) interactable;
                weaponSystem.PutWeaponInHand(weaponPickUp.Interact());
            }
        }

        public void OnInteraction(List<AbstractQuest> quests)
        {
            animationsSystem.PlayInteractAnimation();
            uiController.OpenQuestGiverWindows(quests);
        }

        public void OnInteraction(WeaponConfig weaponConfig)
        {
            weaponSystem.PutWeaponInHand(weaponConfig);
        }

        public void OnInteraction(QuestItem item)
        {
            questSystem.UpdatePickUpQuestsState(item.ItemName);
        }

        public void OnEnemyKilled(string enemyName)
        {
            questSystem.UpdateKillEnemiesQuestsState(enemyName);
        }

        #endregion
        
        #region Private methods

        private void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            // todo temp dirty hack for player positioning after scene load
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(PLAYER_LOAD_POINT);
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }
            
            // re-init outer dependencies as older was destroyed on scene unload
            InitOuterDependencies();
        }

        /* todo    - switch to DI
         * @author - Артур
         * @date   - 29.05.2018
         * @time   - 23:35
        */        
        private void InitOuterDependencies()
        {
            uiController = FindObjectOfType<UIController>();
            HealthState healthState = healthSystem.GetCurrentHealthState();
            uiController.UpdatePlayerHealthBarValues(healthState.CurrentHealth, healthState.MaxHealth);
            
            inputSystem.InitOuterDependencies();
        }

        #endregion
    }
}