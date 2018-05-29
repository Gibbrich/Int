using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FMODUnity;
using Game.Characters.Animations.Scripts;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapons/WeaponConfig")]
[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class WeaponConfig: ScriptableObject
{
    #region Editor tweakable fields
    
    [CanBeNull]
    [SerializeField]
    private GameObject prefab;

    [CanBeNull]
    [SerializeField]
    [Tooltip("Used for tuning correct weapon displaying")]
    private Transform grip;

    [NotNull]
    [SerializeField]
    private WeaponAnimations animations;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float attackRange;
    
    [CanBeNull]
    [EventRef]
    [SerializeField]
    private string attackSound;
    
    #endregion

    #region Properties
    
    public float Damage
    {
        get { return damage; }
    }

    public float Speed
    {
        get { return speed; }
    }

    public float AttackRange
    {
        get { return attackRange; }
    }

    [CanBeNull]
    public GameObject Prefab
    {
        get { return prefab; }
    }

    [CanBeNull]
    public Transform Grip
    {
        get { return grip; }
    }

    [NotNull]
    public WeaponAnimations Animations
    {
        get { return animations; }
    }

    #endregion
    
    #region Public methods

    public void PlayAttackSound()
    {
        if (attackSound != null)
        {
            RuntimeManager.PlayOneShot(attackSound);
        }
    }
    
    #endregion
}