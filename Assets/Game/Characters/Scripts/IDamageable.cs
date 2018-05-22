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
        GameObject GetGameObject();
    }
}