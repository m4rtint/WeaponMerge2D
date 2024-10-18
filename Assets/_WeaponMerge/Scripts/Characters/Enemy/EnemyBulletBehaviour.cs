using System;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyBulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 5f;

        private float _timeToLive;
        private Vector2 _direction;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealthBehaviour playerHealthBehaviour))
            {
                playerHealthBehaviour.TakeDamage(10);
                ObjectPooler.Instance.ReturnToPool(EnemyAttackType.Bullet, gameObject);
            }
        }
        
        public void SpawnAt(Vector2 position, Vector2 direction)
        {
            transform.position = position;
            _direction = direction;
        }

        private void FixedUpdate()
        {
            //Implement move towards direction if time to live is more than 0
            if (_timeToLive > 0)
            {
                _timeToLive -= Time.fixedDeltaTime;
                if (_timeToLive <= 0)
                {
                    ObjectPooler.Instance.ReturnToPool(EnemyAttackType.Bullet, gameObject);
                }
            }
        }
    }
}
