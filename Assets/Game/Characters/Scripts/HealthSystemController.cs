using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Game.Characters.Scripts
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class HealthSystemController : IHealthSystemController
    {
        #region Private fields

        [NotNull]
        private IHealthSystem healthSystem;

        [NotNull]
        private Health health;

        #endregion

        #region Public methods

        public bool TakeDamage(float amount)
        {
            health.CurrentValue -= amount;
            return health.CurrentValue <= 0;
        }

        public void Init(IHealthSystem healthSystem, Health health)
        {
            this.healthSystem = healthSystem;
            this.health = health;
        }

        #endregion
    }

    interface IHealthSystemController : IBaseController<IHealthSystem, Health>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Is <see cref="Health.CurrentValue"/> is lower than 0</param>
        /// <returns></returns>
        bool TakeDamage(float amount);
    }
}