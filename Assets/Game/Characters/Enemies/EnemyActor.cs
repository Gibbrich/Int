using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

[RequireComponent(typeof(HealthSystem))]
[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class EnemyActor : MonoBehaviour, IActor, IDamageable, IPointerClickHandler
{
    #region Private fields

    [NotNull]
    private HealthSystem healthSystem;

    [Inject]
    private PlayerActor player;

    [Inject]
    private UIController uiController;
    
    [NotNull]
    private AnimationsSystem animationsSystem;

    #endregion

    #region Unity callbacks

    // Use this for initialization
    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        animationsSystem = GetComponent<AnimationsSystem>();
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
        
        uiController.UpdateTargetHealthBarValues(healthState.CurrentHealth, healthState.MaxHealth);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            player.SetTarget(this);
            
            uiController.ShowTargetHealthBar();
            HealthState healthState = healthSystem.GetCurrentHealthState();
            uiController.UpdateTargetHealthBarValues(healthState.CurrentHealth, healthState.MaxHealth);
        }
    }

    #endregion
}