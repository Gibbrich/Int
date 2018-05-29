
using System.Diagnostics.CodeAnalysis;
using FMODUnity;
using Game.Characters.Player.Scripts;
using Game.Characters.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(HealthSystem))]
[SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
public class EnemyActor : MonoBehaviour, IActor, IDamageable, IPointerClickHandler
{
    #region Editor tweakable fields

    [SerializeField]
    private string enemyName = "Enemy";
    
    [EventRef]
    [SerializeField]
    private string deathSound;
    
    #endregion
    
    #region Private fields

    [NotNull]
    private HealthSystem healthSystem;

    [NotNull]
    private PlayerActor player;

    [NotNull]
    private UIController uiController;
    
    [NotNull]
    private AnimationsSystem animationsSystem;

    [NotNull]
    private new CapsuleCollider collider;

    #endregion

    #region Unity callbacks

    // Use this for initialization
    void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        healthSystem = GetComponent<HealthSystem>();
        animationsSystem = GetComponent<AnimationsSystem>();
        collider = GetComponent<CapsuleCollider>();
        player = FindObjectOfType<PlayerActor>();
    }

    #endregion

    #region Public methods

    public HealthState TakeDamage(float amount)
    {
        HealthState healthState = healthSystem.TakeDamage(amount);
        
        if (healthState.CurrentHealth <= 0)
        {
            RuntimeManager.PlayOneShot(deathSound);
            animationsSystem.PlayDeathAnimation();
            player.OnEnemyKilled(enemyName);
            collider.isTrigger = true;
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