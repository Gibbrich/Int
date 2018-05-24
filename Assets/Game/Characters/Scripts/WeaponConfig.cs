using System.Collections.Generic;
using Game.Characters.Animations.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapons/WeaponConfig")]
public class WeaponConfig: ScriptableObject
{
    #region Editor tweakable fields
    
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    [Tooltip("Used for tuning correct weapon displaying")]
    private Transform grip;

    [SerializeField]
    private WeaponAnimations animations;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float attackRange;
    
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

    public GameObject Prefab
    {
        get { return prefab; }
    }

    public Transform Grip
    {
        get { return grip; }
    }

    public WeaponAnimations Animations
    {
        get { return animations; }
    }

    #endregion
}