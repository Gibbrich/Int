using UnityEngine;

namespace Game.Characters.Scripts
{
    /// <summary>
    /// This interface should be implemented by <see cref="IActor"/> only.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Is <see cref="Health.CurrentValue"/> is lower than 0</param>
        /// <returns></returns>
        bool TakeDamage(float amount);
        GameObject GetGameObject();
    }
}