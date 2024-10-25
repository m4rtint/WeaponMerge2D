using _WeaponMerge.Scripts.Characters.General;
using _WeaponMerge.Scripts.Environment;
using _WeaponMerge.Scripts.UserInterface;
using _WeaponMerge.Scripts.UserInterface.CoreUI;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BulletBehaviour: MonoBehaviour
    {
        private Rigidbody2D _rigidbody = null;
        private Bullet _bullet = null;
        private Vector2 _moveTowards = Vector2.zero;
        private float _elapsedTimeToLive = 0;
        private int _ownerInstanceId = 0;
        
        protected abstract AmmoType AmmoType { get; }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void SpawnAt(int ownerId, Vector2 position, Bullet bullet, Vector2 moveTowards)
        {
            _ownerInstanceId = ownerId;
            _bullet = bullet;
            transform.position = position;
            _moveTowards = moveTowards;
            _elapsedTimeToLive = _bullet.TimeToLive;
            _rigidbody.linearVelocity = _moveTowards.normalized * _bullet.Speed;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_moveTowards.y, _moveTowards.x) * Mathf.Rad2Deg);
        }
        
        private void FixedUpdate()
        {   
            if (_bullet.TimeToLive > 0)
            {
                _elapsedTimeToLive -= Time.fixedDeltaTime;
                if (_elapsedTimeToLive <= 0)
                {
                    ObjectPooler.Instance.ReturnToPool(AmmoType, gameObject);
                }
            }

            // Raycast and check if it hit something
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _moveTowards.normalized, _bullet.Speed * Time.fixedDeltaTime);
            if (hit.collider != null &&
                hit.collider.gameObject.GetInstanceID() != _ownerInstanceId)
            {
                if (hit.collider.TryGetComponent<DropItemBehaviour>(out _) || 
                    hit.collider.TryGetComponent<DropHealthBehaviour>(out _))
                {
                    return;
                }
                
                // Check if the hit object has a HealthBehaviour component and apply damage
                if (hit.collider.TryGetComponent<HealthBehaviour>(out var enemyHealth))
                {
                    enemyHealth.TakeDamage(_bullet.Damage);
                    DamageNumbersObjectPool.Instance.ShowDamageNumber(_bullet.Damage, enemyHealth.transform.position);
                }
                
                ObjectPooler.Instance.ReturnToPool(AmmoType, gameObject);
            }
        }
    }
}