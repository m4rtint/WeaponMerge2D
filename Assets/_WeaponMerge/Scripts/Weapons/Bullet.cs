namespace _WeaponMerge.Scripts.Weapons
{
    public enum AmmoType
    {
        Simple
    }
    
    public class Bullet
    {
        public float Speed { get; }
        public float Damage { get; }
        public float TimeToLive { get; }

        public Bullet(float speed, float damage, float timeToLive)
        {
            Speed = speed;
            Damage = damage;
            TimeToLive = timeToLive;
        }
    }
}