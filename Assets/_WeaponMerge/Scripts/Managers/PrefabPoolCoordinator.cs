using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Environment;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class PrefabPoolCoordinator : MonoBehaviour
    {
        [Title("Weapons")]
        [SerializeField] private PistolBulletBehaviour _pistolBulletPrefab = null;
        
        [Title("Enemy")]
        [SerializeField] private EnemyBehaviour _enemyPrefab;

        [Title("Drops")]
        [SerializeField] private DropItemBehaviour _weaponDropPrefab;
        [SerializeField] private DropHealthBehaviour _dropHealthPrefab;
        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_enemyPrefab);
            PanicHelper.CheckAndPanicIfNull(_pistolBulletPrefab);
            PanicHelper.CheckAndPanicIfNull(_weaponDropPrefab);
            PanicHelper.CheckAndPanicIfNull(_dropHealthPrefab);
        }

        public void Restart()
        {
            ObjectPooler.Instance.CreatePool(AmmoType.Rifle, _pistolBulletPrefab);
            ObjectPooler.Instance.CreatePool(AmmoType.Pistol, _pistolBulletPrefab);
            ObjectPooler.Instance.CreatePool(EnemyType.Simple, _enemyPrefab);
            ObjectPooler.Instance.CreatePool(DropType.Weapon, _weaponDropPrefab);
            ObjectPooler.Instance.CreatePool(DropType.Health, _dropHealthPrefab);
        }
    }
}
