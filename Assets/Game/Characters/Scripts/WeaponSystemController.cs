using System;
using Game.Scripts;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Characters.Scripts
{
    public class WeaponSystemController: IWeaponSystemController
    {
        #region Private methods

        [NotNull]
        public IWeaponSystem weaponSystem;

        [CanBeNull]
        private Weapon weapon;

        [CanBeNull]
        private IDamageable target;

        private float lastAttackTime = 0;

        #endregion
        
        #region Public methods
        
        public void Attack(IDamageable target)
        {
            if (weapon != null &&
                Time.time - lastAttackTime > weapon.Speed)
            {
                this.target = target;
                weaponSystem.AttackAnimationStart(weapon.AttackAnimations.getRandomItem());
            }
        }

        public void DamageTarget()
        {
            if (weaponSystem.GetRangeToTarget(target.GetGameObject().transform.position) <= weapon.AttackRange)
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

        public void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
        }
        
        /// <summary>
        /// </summary>
        /// <param name="weaponSystem"></param>
        /// <param name="parameters">Always null</param>
        public void Init(IWeaponSystem weaponSystem, object parameters = null)
        {
            this.weaponSystem = weaponSystem;
        }

        #endregion
        
    }

    
    public interface IWeaponSystemController : IBaseController<IWeaponSystem, object>
    {
        /// <summary>
        /// Attacking target divides in 3 steps:
        /// * Check attack conditions and start attack animation <see cref="Attack"/>
        /// * Attack animation plays <see cref="IWeaponSystem.AttackAnimationStart"/>
        /// * Attack animation ends <see cref="IWeaponSystem.AttackAnimationEnds"/> and call <see cref="DamageTarget"/>
        /// </summary>
        /// <param name="target"></param>
        void Attack(IDamageable target);
        void DamageTarget();
        void SetWeapon(Weapon weapon);
    }
}