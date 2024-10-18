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
                        1,
                        "Rifle",
                        0.1f,
                        1f,
                        20f,
                        1,
                        2f,
                        20,
                        0.5f,
                        AmmoType.Rifle);
                case WeaponType.Shotgun:
                    return new Weapon(
                        2,
                        "Shotgun",
                        1f,
                        10f,
                        10f,
                        5,
                        2f,
                        5,
                        0.5f,
                        AmmoType.Shotgun);
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
                id,
                "Pistol",
                fireRate,
                spreadAngle,
                bulletSpeed,
                bulletsPerShot,
                bulletTimeToLive,
                damage,
                penetrateDamageFalloff,
                AmmoType.Pistol);
        }
    }
}