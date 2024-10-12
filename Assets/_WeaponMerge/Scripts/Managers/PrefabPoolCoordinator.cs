using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class PrefabPoolCoordinator : MonoBehaviour
    {
        [Title("Weapons")]
        [SerializeField] private PistolBulletBehaviour _pistolBulletBehaviour = null;
        
        [Title("Enemy")]
        [SerializeField] private EnemyBehaviour _enemyPrefab;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_enemyPrefab);
            PanicHelper.CheckAndPanicIfNull(_pistolBulletBehaviour);
        }

        public void Restart()
        {
            ObjectPooler.Instance.CreatePool(AmmoType.Pistol, _pistolBulletBehaviour);
            ObjectPooler.Instance.CreatePool(EnemyType.Simple, _enemyPrefab);
        }
    }
}
