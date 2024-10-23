using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public abstract class EnemyBehaviour: MonoBehaviour
    {
        protected EnemyPathFindingBehaviour _pathFindingBehaviour = null;
        private EnemyHealthBehaviour _enemyHealthBehaviour = null;
        private Animator _animator = null;
        
        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
            _enemyHealthBehaviour = GetComponent<EnemyHealthBehaviour>();
            _animator = GetComponentInChildren<Animator>();
            
            
            PanicHelper.CheckAndPanicIfNull(_pathFindingBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemyHealthBehaviour);
        }

        public virtual void Initialize(
            PlayerPositionProvider playerPositionProvider,
            EnemyData enemyData,
            Action onDeath,
            Action onCleanUp)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
            _enemyHealthBehaviour.Initialize(enemyData.Health);
            _enemyHealthBehaviour.SetDeathActions(
                onDeathDelay: 1f,
                onDeath: () =>
                {
                    _pathFindingBehaviour.Pause();
                    _animator.SetBool(AnimatorKey.IsDead, true);
                    onDeath?.Invoke();
                    HandleOnDeath();
                },
                onCleanUp: () =>
                {
                    onCleanUp?.Invoke();
                    HandleOnCleanUp();
                    _pathFindingBehaviour.CleanUp();
                    _animator.SetBool(AnimatorKey.IsDead, false);
                    ObjectPooler.Instance.ReturnToPool(enemyData.EnemyType, gameObject);
                });
        }
        
        protected abstract void HandleOnDeath();

        protected abstract void HandleOnCleanUp();
    }
}