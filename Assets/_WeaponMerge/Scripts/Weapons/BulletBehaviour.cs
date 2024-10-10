using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletBehaviour: MonoBehaviour
    {
        private Rigidbody2D _rigidbody = null;
        private Bullet _bullet = null;
        private Vector2 _moveTowards = Vector2.zero;
        private float _elapsedTimeToLive = 0;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void SpawnAt(Vector2 position, Bullet bullet, Vector2 moveTowards)
        {
            _bullet = bullet;
            transform.position = position;
            _moveTowards = moveTowards;
            _elapsedTimeToLive = _bullet.TimeToLive;
            _rigidbody.linearVelocity = _moveTowards.normalized * _bullet.Speed;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_moveTowards.y, _moveTowards.x) * Mathf.Rad2Deg);
        }
        
        private void FixedUpdate()
        {
            //TODO - check if time to live is over, return to to pool when over
            if (_bullet.TimeToLive > 0)
            {
                _elapsedTimeToLive -= Time.fixedDeltaTime;
                if (_bullet.TimeToLive <= 0)
                {
                    ObjectPooler.Instance.ReturnToPool(AmmoType.Simple, gameObject);
                }
            }
            if (Physics2D.Raycast(transform.position, _moveTowards.normalized, _bullet.Speed * Time.fixedDeltaTime).collider != null)
            {
                ObjectPooler.Instance.ReturnToPool(AmmoType.Simple, gameObject);
            }
        }
    }
}