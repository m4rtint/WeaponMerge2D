using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class RangedEnemyBehaviour : MonoBehaviour
    {
        private EnemyPathFindingBehaviour _pathFindingBehaviour = null;
        private EnemyHealthBehaviour _enemyHealthBehaviour = null;
        private EnemyDropBehaviour _enemyDropBehaviour = null;
        private EnemyRangedAttackBehaviour _enemyRangedAttackBehaviour = null;

        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
            _enemyHealthBehaviour = GetComponent<EnemyHealthBehaviour>();
            _enemyDropBehaviour = GetComponent<EnemyDropBehaviour>();
            _enemyRangedAttackBehaviour = GetComponent<EnemyRangedAttackBehaviour>();
            
            PanicHelper.CheckAndPanicIfNull(_pathFindingBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyHealthBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyDropBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyRangedAttackBehaviour);
        }
        
        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
            _enemyHealthBehaviour.Initialize(10, onDeath: () =>
            {
                _enemyDropBehaviour.Drop();
                ObjectPooler.Instance.ReturnToPool(EnemyType.Ranged, gameObject);
            });
            _enemyRangedAttackBehaviour.Initialize(
                pausePathFindingAction: _pathFindingBehaviour.Pause, 
                resumePathFindingAction: _pathFindingBehaviour.Resume, 
                playerPositionProvider: playerPositionProvider
            );        
        }
    }
}
