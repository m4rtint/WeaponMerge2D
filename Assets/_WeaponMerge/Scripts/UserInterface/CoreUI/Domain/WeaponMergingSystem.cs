using System;
using _WeaponMerge.Scripts.Managers.Data;
using _WeaponMerge.Scripts.Weapons;

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
            var id = UnityEngine.Random.Range(10000, 100000);
            return new Weapon(
                id: id, // Randomized id between 10000 and 99999
                name: WeaponDataProvider.Instance.GetWeaponName(id.GetHashCode()),
                image:  WeaponDataProvider.Instance.GetWeaponIcon(id.GetHashCode()), // Assuming image is not provided
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