using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Environment;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _WeaponMerge.Scripts.Managers
{
    public class PrefabPoolCoordinator : MonoBehaviour
    {
        [Title("Weapons")]
        [SerializeField] private PistolBulletBehaviour _pistolBulletPrefab = null;
        
        [FormerlySerializedAs("_enemyPrefab")]
        [Title("Enemy")]
        [SerializeField] private SimpleEnemyBehaviour _simpleEnemyPrefab;
        [SerializeField] private RangedEnemyBehaviour _rangedEnemyPrefab;
        [SerializeField] private EnemyBulletBehaviour _enemyBulletPrefab;
        
        [Title("Drops")]
        [SerializeField] private DropItemBehaviour _weaponDropPrefab;
        [SerializeField] private DropHealthBehaviour _dropHealthPrefab;
        private void Awake()
        {   
            // Weapons
            PanicHelper.CheckAndPanicIfNull(_pistolBulletPrefab);

            // Enemy
            PanicHelper.CheckAndPanicIfNull(_simpleEnemyPrefab);
            PanicHelper.CheckAndPanicIfNull(_rangedEnemyPrefab);
            PanicHelper.CheckAndPanicIfNull(_enemyBulletPrefab);
            
            // Drops
            PanicHelper.CheckAndPanicIfNull(_weaponDropPrefab);
            PanicHelper.CheckAndPanicIfNull(_dropHealthPrefab);
        }

        public void Restart()
        {
            // Weapons
            ObjectPooler.Instance.CreatePool(AmmoType.Rifle, _pistolBulletPrefab);
            ObjectPooler.Instance.CreatePool(AmmoType.Pistol, _pistolBulletPrefab);
            
            // Enemy
            ObjectPooler.Instance.CreatePool(EnemyType.Simple, _simpleEnemyPrefab);
            ObjectPooler.Instance.CreatePool(EnemyType.Ranged, _rangedEnemyPrefab);
            ObjectPooler.Instance.CreatePool(EnemyAttackType.Bullet, _enemyBulletPrefab);
            
            // Drops
            ObjectPooler.Instance.CreatePool(DropType.Weapon, _weaponDropPrefab);
            ObjectPooler.Instance.CreatePool(DropType.Health, _dropHealthPrefab);
        }
    }
}
