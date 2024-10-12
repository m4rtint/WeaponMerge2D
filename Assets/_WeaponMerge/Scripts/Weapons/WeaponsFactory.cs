namespace _WeaponMerge.Scripts.Weapons
{
    public class WeaponsFactory
    {
        public Weapon CreateWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    return new Weapon(
                        0,
                        "Pistol",
                        0.5f,
                        5f,
                        10f,
                        1,
                        2f,
                        10,
                        0.5f,
                        AmmoType.Pistol);
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
    }
}