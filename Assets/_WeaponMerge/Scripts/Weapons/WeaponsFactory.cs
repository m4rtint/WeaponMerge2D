using _WeaponMerge.Scripts.Managers.Data;
using Random = UnityEngine.Random;

namespace _WeaponMerge.Scripts.Weapons
{
    public class WeaponsFactory
    {
        public Weapon CreateWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    return CreatePistol();
                case WeaponType.Rifle:
                    return new Weapon(
                        id: 1,
                        name: "Rifle",
                        sprite: null,
                        fireRate: 0.1f,
                        spreadAngle: 1f,
                        bulletSpeed: 20f,
                        bulletsPerShot: 1,
                        bulletTimeToLive: 2f,
                        damage: 20,
                        penetrateDamageFalloff: 0.5f,
                        ammoType: AmmoType.Rifle);
                case WeaponType.Shotgun:
                    return new Weapon(
                        id: 2,
                        name: "Shotgun",
                        sprite: null,
                        fireRate: 1f,
                        spreadAngle: 10f,
                        bulletSpeed: 10f,
                        bulletsPerShot: 5,
                        bulletTimeToLive: 2f,
                        damage: 5,
                        penetrateDamageFalloff: 0.5f,
                        ammoType: AmmoType.Shotgun);
                default:
                    return null;
            }
        }

        private Weapon CreatePistol()
        {
            // Random Id Created
            int id = Random.Range(1000000, 9999999);
            float fireRate = Random.Range(0.1f, 1.0f); // Example range for fire rate
            float spreadAngle = Random.Range(1f, 10f); // Example range for spread angle
            float bulletSpeed = Random.Range(5f, 20f); // Example range for bullet speed
            int bulletsPerShot = 1;
            float bulletTimeToLive = Random.Range(1f, 5f); // Example range for bullet time to live
            int damage = Random.Range(5, 20); // Example range for damage
            float penetrateDamageFalloff = Random.Range(0.1f, 1f); // Example range for penetrate damage falloff
            
            return new Weapon(
                id: id,
                name: WeaponDataProvider.Instance.GetWeaponName(id.GetHashCode()),
                sprite: WeaponDataProvider.Instance.GetWeaponIcon(id.GetHashCode()), 
                fireRate: fireRate,
                spreadAngle: spreadAngle,
                bulletSpeed: bulletSpeed,
                bulletsPerShot: bulletsPerShot,
                bulletTimeToLive: bulletTimeToLive,
                damage: damage,
                penetrateDamageFalloff: penetrateDamageFalloff,
                ammoType: AmmoType.Pistol);
        }
    }
}