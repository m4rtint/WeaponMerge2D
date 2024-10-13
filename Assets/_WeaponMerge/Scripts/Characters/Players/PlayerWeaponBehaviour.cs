using System;
using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform _weaponTip = null;
        private Weapon _equippedWeapon = null;

        private void Awake()
        {
        }

        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnShootAction += Shoot;
            controlInput.OnScrollWeaponAction += ScrollWeapon;
        }

        private void ScrollWeapon(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                
            }
        }

        private void Shoot(bool onShoot)
        {
            if (onShoot)
            {
                // Get the bullet from the object pool
                var bullet = ObjectPooler.Instance.Get<BulletBehaviour>(_equippedWeapon.AmmoType);

                // Get the mouse position in world space
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Remove the Z axis (since we're in 2D space)
                mousePosition.z = 0f;

                // Calculate the direction from the weapon tip to the mouse position
                Vector2 direction = (mousePosition - _weaponTip.position).normalized;

                // Set up the bullet properties
                var bulletProperties = new Bullet(
                    _equippedWeapon.BulletSpeed,
                    _equippedWeapon.Damage,
                    _equippedWeapon.BulletTimeToLive
                );

                // Spawn the bullet at the weapon tip, moving towards the mouse
                bullet.SpawnAt(
                    ownerId: gameObject.GetInstanceID(),
                    _weaponTip.position,
                    bulletProperties,
                    direction);
            }
        }
        
        public void Restart()
        {
            var weaponsFactory = new WeaponsFactory();
            _equippedWeapon = weaponsFactory.CreateWeapon(WeaponType.Pistol);
        }
        
        public void CleanUp()
        {
            _equippedWeapon = null;
        }
    }
}