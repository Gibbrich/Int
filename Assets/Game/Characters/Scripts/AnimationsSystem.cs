using System.Diagnostics.CodeAnalysis;
using Game.Characters.Animations.Scripts;
using Game.Scripts;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class AnimationsSystem : MonoBehaviour
{
    private const string DEFAULT_ATTACK = "DefaultAttack";
    private const string DEFAULT_HIT = "DefaultHit";
    private const string DEFAULT_DEATH = "DefaultDeath";
    private const string DEFAULT_INTERACT = "DefaultInteract";

    private const string TRIGGER_INTERACT = "OnInteraction";
    private const string TRIGGER_ATTACK = "OnAttack";
    private const string TRIGGER_DEATH = "OnDeath";
    private const string TRIGGER_HIT = "OnHit";
    
    #region Private fields
    
    [NotNull]
    private Animator animator;

    [NotNull]
    [SerializeField]
    private AnimatorOverrideController animatorOverrideController;

    [CanBeNull]
    private WeaponAnimations animations;
    
    #endregion
    
    #region Unity callbacks
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
            
//        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }
    
    #endregion

    #region Public methods

    public void UpdateAnimationsSet(WeaponAnimations animations)
    {
        this.animations = animations;
    }
    
    public void PlayAttackAnimation()
    {
        if (animations != null)
        {
            animatorOverrideController[DEFAULT_ATTACK] = animations.AttackAnimations.GetRandomItem();
        }
        animator.SetTrigger(TRIGGER_ATTACK);
    }

    public void PlayHitAnimation()
    {
        if (animations != null)
        {
            animatorOverrideController[DEFAULT_HIT] = animations.TakeHitAnimations.GetRandomItem();
        }
        animator.SetTrigger(TRIGGER_HIT);
    }
    
    public void PlayDeathAnimation()
    {
        if (animations != null)
        {
            animatorOverrideController[DEFAULT_DEATH] = animations.DeathAnimations.GetRandomItem();
        }
        animator.SetTrigger(TRIGGER_DEATH);
    }
    
    public void PlayInteractAnimation()
    {
        if (animations != null)
        {
            animatorOverrideController[DEFAULT_INTERACT] = animations.InteractAnimations.GetRandomItem();
        }
        animator.SetTrigger(TRIGGER_INTERACT);
    }
    
    #endregion
}