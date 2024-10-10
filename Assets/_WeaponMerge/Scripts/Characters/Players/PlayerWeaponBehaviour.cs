using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Players
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField] 
        private BulletBehaviour _bulletPrefab = null;
        [SerializeField]
        private Transform _weaponTip = null;
        
        public void Initialize(ControlInput controlInput)
        {
            ObjectPooler.Instance.CreatePool(AmmoType.Simple, _bulletPrefab);
            controlInput.OnShootAction += Shoot;
        }
        
        private void Shoot(bool onShoot)
        {
            if (onShoot)
            {
                // Get the bullet from the object pool
                BulletBehaviour bullet = ObjectPooler.Instance.Get<BulletBehaviour>(AmmoType.Simple);

                // Get the mouse position in world space
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Remove the Z axis (since we're in 2D space)
                mousePosition.z = 0f;

                // Calculate the direction from the weapon tip to the mouse position
                Vector2 direction = (mousePosition - _weaponTip.position).normalized;

                // Set up the bullet properties
                var properties = new Bullet(10, 10, 3f);

                // Spawn the bullet at the weapon tip, moving towards the mouse
                bullet.SpawnAt(_weaponTip.position, properties, direction);
            }
        }
    }
}
