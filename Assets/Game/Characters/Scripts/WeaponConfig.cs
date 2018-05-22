using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfig: ScriptableObject
{
    #region Private fields

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    [Tooltip("Used for tuning correct weapon displaying")]
    private Transform grip;

    [SerializeField]
    private List<AnimationClip> attackAnimations;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float attackRange;

    #endregion

    #region Properties
    
    public List<AnimationClip> AttackAnimations
    {
        get { return attackAnimations; }
    }

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

    public GameObject Prefab
    {
        get { return prefab; }
    }

    public Transform Grip
    {
        get { return grip; }
    }

    #endregion
}