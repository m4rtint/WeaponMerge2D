using System;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.General
{
    public abstract class HealthBehaviour : MonoBehaviour
    {
        private Action _onDeath;
        
        protected int MaxHealth { get; set; }
        private int _health;

        protected int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnHealthChanged();
            }
        }
        
        public void Initialize(int health, Action onDeath)
        {
            MaxHealth = health;
            Health = health;
            _onDeath = onDeath;
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (_health <= 0)
            {
                OnDeath();
            }
        }
        
        public void GainHealth(int health)
        {
            Health = Mathf.Min(Health + health, MaxHealth);
        }

        protected abstract void OnHealthChanged();

        protected virtual void OnDeath()
        {
            _onDeath?.Invoke();
        }
    }
}
