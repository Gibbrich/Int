using System.Diagnostics.CodeAnalysis;
using Game.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Characters.Scripts
{
    [RequireComponent(typeof(Animator))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class WeaponSystem : MonoBehaviour
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        [Tooltip("Weapon object parent")]
        private Transform weaponSocket;
    
        #endregion
    
        #region Private fields

        [CanBeNull]
        private GameObject weaponObject;
        
        [CanBeNull]
        private WeaponConfig weaponConfig;

        [NotNull]
        private Animator animator;

        [NotNull]
        private AnimatorOverrideController animatorOverrideController;
        
        [CanBeNull]
        private IDamageable target;

        private float lastAttackTime = 0;

        #endregion
        
        #region Unity callbacks
        
        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;
        }
        
        #endregion

        #region Public methods

        public void PutWeaponInHand(WeaponConfig weaponConfig)
        {
            this.weaponConfig = weaponConfig;
            Destroy(weaponObject);
        
            weaponObject = Instantiate(weaponConfig.Prefab, weaponSocket);
            weaponObject.transform.localPosition = weaponConfig.Grip.localPosition;
            weaponObject.transform.localRotation = weaponConfig.Grip.localRotation;
        }

        public void AttackAnimationStart(AnimationClip clip)
        {
            animatorOverrideController["DefaultAttack"] = clip;
            animator.SetTrigger("OnAttack");
        }

        public float GetRangeToTarget(Vector3 targetPosition)
        {
            return Vector3.Distance(gameObject.transform.position, targetPosition);
        }

        /// <summary>
        /// Called by <see cref="animator"/>
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void AttackAnimationEnds()
        {
            DamageTarget();
        }

        /// <summary>
        /// Attacking target divides in 3 steps:
        /// * Check attack conditions and start attack animation <see cref="Attack"/>
        /// * Attack animation plays <see cref="AttackAnimationStart"/>
        /// * Attack animation ends <see cref="AttackAnimationEnds"/> and call <see cref="DamageTarget"/>
        /// </summary>
        public void Attack()
        {            
            if (target != null &&
                weaponConfig != null &&
                Time.time - lastAttackTime > weaponConfig.Speed)
            {
                AttackAnimationStart(weaponConfig.Animations.AttackAnimations.getRandomItem());
                lastAttackTime = Time.time;
            }
        }
        
        public void SetTarget(IDamageable target)
        {
            this.target = target;
        }
        
        #endregion
        
        #region Private methods
        
        private void DamageTarget()
        {
            if (GetRangeToTarget(target.GetGameObject().transform.position) <= weaponConfig.AttackRange)
            {
                /* todo    - parametrize damage value
                 * @author - Артур
                 * @date   - 20.05.2018
                 * @time   - 19:52
                */                
                target.TakeDamage(100);
            }
        }        
        
        #endregion
    }
}