using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Characters.Scripts;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class EnemyActor : MonoBehaviour, IActor, IDamageable
{
    #region Private fields

    [NotNull]
    private HealthSystem healthSystem;
    
    #endregion
    
    #region Unity callbacks
    
    // Use this for initialization
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
    
    #endregion
    
    #region Public methods
    
    public bool TakeDamage(float amount)
    {
        return healthSystem.TakeDamage(amount);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
    
    #endregion
}