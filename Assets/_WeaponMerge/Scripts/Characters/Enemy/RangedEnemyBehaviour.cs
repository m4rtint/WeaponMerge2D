using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
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
        private EnemyRangedAttackBehaviour _enemyRangedAttackBehaviour = null;
        private Animator _animator = null;

        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
            _enemyHealthBehaviour = GetComponent<EnemyHealthBehaviour>();
            _enemyRangedAttackBehaviour = GetComponent<EnemyRangedAttackBehaviour>();
            _animator = GetComponentInChildren<Animator>();
            
            PanicHelper.CheckAndPanicIfNull(_pathFindingBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyHealthBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyRangedAttackBehaviour);
        }
        
        public void Initialize(
            PlayerPositionProvider playerPositionProvider,
            EnemyData data,
            Action onDeath, 
            Action onCleanUp)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
            _enemyHealthBehaviour.Initialize(data.Health);
            _enemyHealthBehaviour.SetDeathActions(
                onDeathDelay: 1f, 
                onDeath: () =>
                {
                    onDeath?.Invoke();
                    _animator?.SetBool(AnimatorKey.IsDead, true);
                    _pathFindingBehaviour.Pause();
                    _enemyRangedAttackBehaviour.StopAttack();
                }, 
                onCleanUp: () => 
                {
                    onCleanUp?.Invoke();
                    _animator?.SetBool(AnimatorKey.IsDead, false);
                    _pathFindingBehaviour.CleanUp();
                    _enemyRangedAttackBehaviour.CleanUp();
                    ObjectPooler.Instance.ReturnToPool(EnemyType.Ranged, gameObject); 
                });
            _enemyRangedAttackBehaviour.Initialize(
                pausePathFindingAction: _pathFindingBehaviour.Pause, 
                resumePathFindingAction: _pathFindingBehaviour.Resume, 
                playerPositionProvider: playerPositionProvider,
                damage: data.Damage
            );        
        }
    }
}
