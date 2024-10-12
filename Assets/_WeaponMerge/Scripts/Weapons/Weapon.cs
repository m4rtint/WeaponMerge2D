namespace _WeaponMerge.Scripts.Weapons
{
    public enum WeaponType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    
    public class Weapon
    {
        public float ShootRate;
        public float SpreadAngle;
        public float BulletSpeed;
        public int BulletsPerShot;
        public float BulletTimeToLive;
        public int Damage;
        public float PenetrateDamageFalloff;
        
        public AmmoType AmmoType;
    }
}