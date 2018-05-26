using System;
using UnityEngine;

namespace Game.Characters.Scripts
{
    /// <summary>
    /// This interface should be implemented by <see cref="IActor"/> only.
    /// </summary>
    public interface IDamageable
    {
        HealthState TakeDamage(float amount);
        HealthState GetCurrentHealthState();
        GameObject GetGameObject();
    }

    public static class IDamageableExtensions
    {
        public static bool IsAlive(this IDamageable damageable)
        {
            return damageable.GetCurrentHealthState().CurrentHealth > 0;
        }
    }
}