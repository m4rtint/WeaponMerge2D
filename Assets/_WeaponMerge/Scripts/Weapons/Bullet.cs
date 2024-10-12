namespace _WeaponMerge.Scripts.Weapons
{
    public enum AmmoType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    
    public class Bullet
    {
        public float Speed { get; }
        public int Damage { get; }
        public float TimeToLive { get; }

        public Bullet(float speed, int damage, float timeToLive)
        {
            Speed = speed;
            Damage = damage;
            TimeToLive = timeToLive;
        }
    }
}