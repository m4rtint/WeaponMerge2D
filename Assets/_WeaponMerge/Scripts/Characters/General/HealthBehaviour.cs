using System;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.General
{
    public abstract class HealthBehaviour : MonoBehaviour
    {
        private Action _onDeath;
        private Action _onCleanUp;
        
        private bool _isDying = false;
        private float _onDeathDelay = 0f;
        private float _elapsedDyingTime = 0f;

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

        public void Initialize(int health)
        {
            MaxHealth = health;
            Health = health;
            
        }

        public void SetDeathActions(float onDeathDelay, Action onDeath, Action onCleanUp)
        {
            _onDeath = onDeath;
            _onCleanUp = onCleanUp;
            _onDeathDelay = onDeathDelay;
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
            _isDying = true;
        }
        
        private void Update()
        {
            if (_isDying)
            {
                _elapsedDyingTime += Time.deltaTime;
                if (_elapsedDyingTime >= _onDeathDelay)
                {
                    _isDying = false;
                    _onCleanUp?.Invoke();
                    _elapsedDyingTime = 0f;
                }
            }
        }
    }
}
