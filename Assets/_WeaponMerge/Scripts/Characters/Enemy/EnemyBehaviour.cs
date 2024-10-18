using System;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Environment;
using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyPathFindingBehaviour _pathFindingBehaviour = null;
        private EnemyHealthBehaviour _enemyHealthBehaviour = null;
        private EnemyDropBehaviour _enemyDropBehaviour = null;

        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
            _enemyHealthBehaviour = GetComponent<EnemyHealthBehaviour>();
            _enemyDropBehaviour = GetComponent<EnemyDropBehaviour>();
            
            PanicHelper.CheckAndPanicIfNull(_pathFindingBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyHealthBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyDropBehaviour);
        }
        
        public void Initialize(PlayerPositionProvider playerPositionProvider, Action onDeath)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
            _enemyHealthBehaviour.Initialize(10, onDeath: () =>
            {
                onDeath?.Invoke();
                _enemyDropBehaviour.Drop();
                ObjectPooler.Instance.ReturnToPool(EnemyType.Simple, gameObject);
            });
        }
    }
}
