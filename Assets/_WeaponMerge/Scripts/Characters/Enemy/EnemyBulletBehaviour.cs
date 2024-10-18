using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _timeToLive = 5f;
        [SerializeField] private int _damage = 10;

        private Rigidbody2D _rigidbody = null;
        private float _elapsedTimeToLive;
        private Vector2 _direction = Vector2.zero;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
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
            _elapsedTimeToLive = _timeToLive;
            _rigidbody.linearVelocity = _direction.normalized * _speed;
        }

        private void FixedUpdate()
        {
            if (_elapsedTimeToLive > 0)
            {
                _elapsedTimeToLive -= Time.fixedDeltaTime;
                if (_elapsedTimeToLive <= 0)
                {
                    ObjectPooler.Instance.ReturnToPool(EnemyAttackType.Bullet, gameObject);
                }
            }
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction.normalized, _speed * Time.fixedDeltaTime);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out PlayerHealthBehaviour playerHealthBehaviour))
                {
                    playerHealthBehaviour.TakeDamage(_damage);
                    ObjectPooler.Instance.ReturnToPool(EnemyAttackType.Bullet, gameObject);
                }
            }
        }
    }
}
