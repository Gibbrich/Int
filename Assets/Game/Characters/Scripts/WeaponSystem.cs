using System.Diagnostics.CodeAnalysis;
using Game.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Characters.Scripts
{
    [RequireComponent(typeof(AnimationsSystem))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class WeaponSystem : MonoBehaviour
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        [Tooltip("Weapon object parent")]
        private Transform weaponSocket;
        
        [NotNull]
        [SerializeField]
        private WeaponConfig weaponConfig;
    
        #endregion
    
        #region Private fields

        [CanBeNull]
        private GameObject weaponObject;

        [NotNull]
        private AnimationsSystem animationsSystem;
        
        [CanBeNull]
        private IDamageable target;

        private float lastAttackTime = 0;

        #endregion
        
        #region Unity callbacks
        
        // Use this for initialization
        void Start()
        {
            animationsSystem = GetComponent<AnimationsSystem>();
            animationsSystem.UpdateAnimationsSet(weaponConfig.Animations);
        }
        
        #endregion

        #region Public methods

        public void PutWeaponInHand(WeaponConfig weaponConfig)
        {
            this.weaponConfig = weaponConfig;
            animationsSystem.UpdateAnimationsSet(weaponConfig.Animations);
            
            Destroy(weaponObject);

            if (weaponConfig.Prefab != null)
            {
                weaponObject = Instantiate(weaponConfig.Prefab, weaponSocket);

                if (weaponConfig.Grip != null)
                {
                    weaponObject.transform.localPosition = weaponConfig.Grip.localPosition;
                    weaponObject.transform.localRotation = weaponConfig.Grip.localRotation;
                }
            }
        }

        public float GetRangeToTarget(Vector3 targetPosition)
        {
            return Vector3.Distance(gameObject.transform.position, targetPosition);
        }

        /// <summary>
        /// Called by <see cref="Animator"/>
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void AttackAnimationEnds()
        {
            DamageTarget();
        }

        /// <summary>
        /// Attacking target divides in 3 steps:
        /// * Check attack conditions and start attack animation <see cref="Attack"/>
        /// * Attack animation plays <see cref="AnimationsSystem.PlayAttackAnimation"/>
        /// * Attack animation ends <see cref="AttackAnimationEnds"/> and call <see cref="DamageTarget"/>
        /// </summary>
        public void Attack()
        {            
            if (target != null &&
                Time.time - lastAttackTime > weaponConfig.Speed)
            {
                animationsSystem.PlayAttackAnimation();
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
                target.TakeDamage(weaponConfig.Damage);
            }
        }        
        
        #endregion
    }
}