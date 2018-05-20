using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Characters.Scripts
{
    [RequireComponent(typeof(Animator))]
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class WeaponSystem : MonoBehaviour, IWeaponSystem
    {
        #region Editor tweakable fields

        [NotNull]
        [SerializeField]
        private Transform weaponSocket;
    
        #endregion
    
        #region Private fields

        [NotNull]
        private IWeaponSystemController weaponSystemController;

        [CanBeNull]
        private GameObject currentWeapon;

        [NotNull]
        private Animator animator;

        #endregion

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        #region Public methods

        public void PutWeaponInHand(Weapon weapon)
        {
            weaponSystemController.SetWeapon(weapon);
        
            Destroy(currentWeapon);
        
            currentWeapon = Instantiate(weapon.Prefab, weaponSocket);
            currentWeapon.transform.localPosition = weapon.Grip.localPosition;
            currentWeapon.transform.localRotation = weapon.Grip.localRotation;
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
            weaponSystemController.DamageTarget();
        }

        public void Attack(IDamageable target)
        {
            weaponSystemController.Attack(target);
        }

        #endregion
        
        #region Private methods
        
        [Inject]
        private void Init(IWeaponSystemController weaponSystemController)
        {
            this.weaponSystemController = weaponSystemController;
            weaponSystemController.Init(this);
        }        
        
        #endregion
    }

    public interface IWeaponSystem: IBaseSystem
    {
        void PutWeaponInHand([NotNull] Weapon weapon);
        void AttackAnimationStart([NotNull] AnimationClip clip);
        float GetRangeToTarget(Vector3 targetPosition);
        void AttackAnimationEnds();
        void Attack(IDamageable target);
    }
}