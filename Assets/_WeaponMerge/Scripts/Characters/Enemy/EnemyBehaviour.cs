using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyPathFindingBehaviour _pathFindingBehaviour = null;
        private EnemyHealthBehaviour _enemyHealthBehaviour = null;
        private SimpleEnemyAttackBehaviour _simpleEnemyAttackBehaviour = null;

        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
            _enemyHealthBehaviour = GetComponent<EnemyHealthBehaviour>();
            _simpleEnemyAttackBehaviour = GetComponent<SimpleEnemyAttackBehaviour>();
            
            PanicHelper.CheckAndPanicIfNull(_pathFindingBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyHealthBehaviour);
            PanicHelper.CheckAndPanicIfNull(_simpleEnemyAttackBehaviour);
        }
        
        public void Initialize(
            PlayerPositionProvider playerPositionProvider,
            EnemyData data,
            Action onDeath)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
            _enemyHealthBehaviour.Initialize(data.Health, onDeath: () =>
            {
                onDeath?.Invoke();
                ObjectPooler.Instance.ReturnToPool(EnemyType.Simple, gameObject);
            });
            _simpleEnemyAttackBehaviour.Initialize(damage: data.Damage);
        }
    }
}
