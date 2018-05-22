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
        
        [CanBeNull]
        private IDamageable target;

        private float lastAttackTime = 0;

        #endregion
        
        #region Unity callbacks
        
        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
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
            /* todo    - implement using animator
             * @author - Артур
             * @date   - 20.05.2018
             * @time   - 18:52
            */            
        }

        public float GetRangeToTarget(Vector3 targetPosition)
        {
            return Vector3.Distance(gameObject.transform.position, targetPosition);
        }

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
        /// <param name="target"></param>
        public void Attack(IDamageable target)
        {            
            if (target != null &&
                weaponConfig != null &&
                Time.time - lastAttackTime > weaponConfig.Speed)
            {
                AttackAnimationStart(weaponConfig.AttackAnimations.getRandomItem());
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
                lastAttackTime = Time.time;
            }

            target = null;
        }        
        
        #endregion
    }
}