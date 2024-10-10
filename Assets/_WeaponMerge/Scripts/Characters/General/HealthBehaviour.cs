using System;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.General
{
    public abstract class HealthBehaviour : MonoBehaviour
    {
        private int _health;
        
        public void Initialize(int health)
        {
            _health = health;
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }
        
        public void GainHealth(int health)
        {
            _health += health;
        }

        public virtual void Die()
        {
        }
    }
}
