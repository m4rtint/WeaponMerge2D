namespace _WeaponMerge.Scripts.Weapons
{
    public class WeaponsFactory
    {
        public Weapon CreateWeapon(WeaponType weaponType)
        {
            return weaponType switch
            {
                WeaponType.Pistol => new Weapon
                {
                    ShootRate = 0.5f,
                    SpreadAngle = 5f,
                    BulletSpeed = 10f,
                    BulletsPerShot = 1,
                    BulletTimeToLive = 2f,
                    Damage = 10,
                    PenetrateDamageFalloff = 0.5f,
                    AmmoType = AmmoType.Pistol
                },
                WeaponType.Rifle => new Weapon
                {
                    ShootRate = 0.1f,
                    SpreadAngle = 2f,
                    BulletSpeed = 20f,
                    BulletsPerShot = 1,
                    BulletTimeToLive = 2f,
                    Damage = 5,
                    PenetrateDamageFalloff = 0.5f,
                    AmmoType = AmmoType.Rifle
                },
                WeaponType.Shotgun => new Weapon
                {
                    ShootRate = 1f,
                    SpreadAngle = 10f,
                    BulletSpeed = 5f,
                    BulletsPerShot = 5,
                    BulletTimeToLive = 2f,
                    Damage = 20,
                    PenetrateDamageFalloff = 0.5f,
                    AmmoType = AmmoType.Shotgun
                },
                _ => null
            };
        }
    }
}