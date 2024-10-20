using _WeaponMerge.Scripts.Characters.Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class SimpleEnemyAttackBehaviour: MonoBehaviour
    {
        [SerializeField, ReadOnly] private int _damage;
        [SerializeField] private float _attackRateInSeconds = 1f;
        private float _attackCoolDown = 0f;
        private bool _canAttack = true;
        
        private bool CanAttack => _attackCoolDown <= 0f && _canAttack;

        public void Initialize(int damage)
        {
            _damage = damage;
        }

        public void StopAttack()
        {
            _canAttack = false;
        }

        public void CleanUp()
        {
            _attackCoolDown = 0f;
            _canAttack = true;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            AttackIfPossible(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            AttackIfPossible(other);
        }

        private void AttackIfPossible(Collision2D other)
        {
            if (CanAttack && 
                other.gameObject.TryGetComponent(out PlayerHealthBehaviour playerHealthBehaviour))
            {
                playerHealthBehaviour.TakeDamage(_damage);
                _attackCoolDown = _attackRateInSeconds;
            }
        }

        private void Update()
        {
            if (_attackCoolDown > 0f)
            {
                _attackCoolDown -= Time.deltaTime;
            }
        }
    }
}