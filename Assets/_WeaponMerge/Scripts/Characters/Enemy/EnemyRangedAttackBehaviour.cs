using System;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyRangedAttackBehaviour : MonoBehaviour
    {
        [SerializeField] private float _shootingRange = 8f;
        [SerializeField] private float _bufferRange = 13f;
        [SerializeField] private float _shootCooldown = 1f;

        private Action _pausePathFindingAction;
        private Action _resumePathFindingAction;
        private PlayerPositionProvider _playerPositionProvider;
        private int _damage;
        private float _shootCooldownTimer = 0f;

        public void Initialize(
            Action pausePathFindingAction, 
            Action resumePathFindingAction, 
            PlayerPositionProvider playerPositionProvider,
            int damage)
        {
            _pausePathFindingAction = pausePathFindingAction;
            _resumePathFindingAction = resumePathFindingAction;
            _playerPositionProvider = playerPositionProvider;
            _damage = damage;
        }

        private void Update()
        {
            if (_playerPositionProvider == null)
            {
                return;
            }

            HandleMovementAndShooting();
            UpdateShootCooldown();
        }

        private void HandleMovementAndShooting()
        {
            Vector3 playerPosition = _playerPositionProvider.Get();
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            if (distanceToPlayer > _bufferRange)
            {
                _resumePathFindingAction?.Invoke();
            }
            else if (distanceToPlayer <= _shootingRange)
            {
                _pausePathFindingAction?.Invoke();
                if (_shootCooldownTimer <= 0f)
                {
                    Shoot();
                    _shootCooldownTimer = _shootCooldown;
                }
            }
        }

        private void UpdateShootCooldown()
        {
            if (_shootCooldownTimer > 0f)
            {
                _shootCooldownTimer -= Time.deltaTime;
            }
        }

        private void Shoot()
        {
            // Implement shooting logic here
            var bullet = ObjectPooler.Instance.Get<EnemyBulletBehaviour>(EnemyAttackType.Bullet);
            bullet.SpawnAt(
                position: transform.position,
                direction: (_playerPositionProvider.Get() - transform.position).normalized, 
                damage: _damage);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _shootingRange);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _bufferRange);
        }
    }
}