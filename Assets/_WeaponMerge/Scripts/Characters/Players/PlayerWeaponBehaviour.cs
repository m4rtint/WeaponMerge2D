using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform _weaponTip = null;
        private Weapon _equipedWeapon = null;

        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnShootAction += Shoot;
        }
        
        private void Shoot(bool onShoot)
        {
            if (onShoot)
            {
                // Get the bullet from the object pool
                //TODO - shouldn't be pistol bullets
                var bullet = ObjectPooler.Instance.Get<BulletBehaviour>(_equipedWeapon.AmmoType);

                // Get the mouse position in world space
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Remove the Z axis (since we're in 2D space)
                mousePosition.z = 0f;

                // Calculate the direction from the weapon tip to the mouse position
                Vector2 direction = (mousePosition - _weaponTip.position).normalized;

                // Set up the bullet properties
                var bulletProperties = new Bullet(
                    _equipedWeapon.BulletSpeed,
                    _equipedWeapon.Damage,
                    _equipedWeapon.BulletTimeToLive
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
            _equipedWeapon = weaponsFactory.CreateWeapon(WeaponType.Pistol);
        }
        
        public void CleanUp()
        {
            _equipedWeapon = null;
        }
    }
}
