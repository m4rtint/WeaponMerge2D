using _WeaponMerge.Scripts.Inventory;

namespace _WeaponMerge.Scripts.Weapons
{
    public enum WeaponType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    
    public class Weapon : Item
    {
        public float ShootRate { get; private set; }
        public float SpreadAngle { get; private set; }
        public float BulletSpeed { get; private set; }
        public int BulletsPerShot { get; private set; }
        public float BulletTimeToLive { get; private set; }
        public int Damage { get; private set; }
        public float PenetrateDamageFalloff { get; private set; }
        public AmmoType AmmoType { get; private set; }

        public Weapon(
            int id,
            string name,
            float shootRate,
            float spreadAngle,
            float bulletSpeed,
            int bulletsPerShot,
            float bulletTimeToLive,
            int damage,
            float penetrateDamageFalloff,
            AmmoType ammoType) : base(id, name)
        {
            ShootRate = shootRate;
            SpreadAngle = spreadAngle;
            BulletSpeed = bulletSpeed;
            BulletsPerShot = bulletsPerShot;
            BulletTimeToLive = bulletTimeToLive;
            Damage = damage;
            PenetrateDamageFalloff = penetrateDamageFalloff;
            AmmoType = ammoType;
        }
    }
}