using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public float maxHP = 1f;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        float currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment(float value)
        {
            currentHP = Mathf.Clamp(currentHP + value, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(float value)
        {
            currentHP = Mathf.Clamp(currentHP - value, 0, maxHP);
            //if (currentHP == 0)
            //{
            //    var ev = Schedule<HealthIsZero>();
            //    ev.health = this;
            //}
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement(1f);
        }

        void Awake()
        {
            currentHP = maxHP;
        }
    }
}
