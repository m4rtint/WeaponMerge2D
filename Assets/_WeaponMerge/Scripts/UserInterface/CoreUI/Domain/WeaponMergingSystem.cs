using System;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain
{
    public class WeaponMergingSystem
    {
        private enum MergeType
        {
            Higher,
            Lower
        }

        public Weapon MergePistols(Weapon weaponOne, Weapon weaponTwo)
        {
            return new Weapon(
                id: UnityEngine.Random.Range(10000, 100000), // Randomized id between 10000 and 99999
                name: PlaceholderWeaponNames.GetRandomPistolName(),
                fireRate: (weaponOne.FireRate + weaponTwo.FireRate) / 2,
                spreadAngle: Randomize(weaponOne.SpreadAngle, weaponTwo.SpreadAngle, MergeType.Higher),
                bulletSpeed: Randomize(weaponOne.BulletSpeed, weaponTwo.BulletSpeed, MergeType.Higher),
                bulletsPerShot: 1,
                bulletTimeToLive: Randomize(weaponOne.BulletTimeToLive, weaponTwo.BulletTimeToLive, MergeType.Higher),
                damage: (int)Randomize(weaponOne.Damage, weaponTwo.Damage, MergeType.Higher),
                penetrateDamageFalloff: Randomize(weaponOne.PenetrateDamageFalloff, weaponTwo.PenetrateDamageFalloff, MergeType.Higher),
                ammoType: weaponOne.AmmoType // Assuming both weapons have the same AmmoType
            );
        }

        private float Randomize(float valueOne, float valueTwo, MergeType mergeType)
        {
            var higherValue = Math.Max(valueOne, valueTwo);
            var range = Math.Abs(valueOne - valueTwo);
            var baseValue = higherValue;
            var randomize = UnityEngine.Random.Range(0, range);
            if (mergeType == MergeType.Higher)
            {
                return baseValue + randomize;
            }
            return baseValue - randomize;
        }
    }
}