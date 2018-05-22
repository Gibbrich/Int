﻿using System;
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

    #endregion

    #region Unity callbacks

    // Use this for initialization
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
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