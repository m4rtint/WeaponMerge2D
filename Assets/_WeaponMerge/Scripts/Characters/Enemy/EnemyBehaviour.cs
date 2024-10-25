using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public abstract class EnemyBehaviour: MonoBehaviour
    {
        private EnemyPathFindingBehaviour _pathFindingBehaviour;
        private EnemyHealthBehaviour _enemyHealthBehaviour;
        private Collider2D _collider2D;
        private Animator _animator;
        
        private Collider2D Collider2D
        {
            get
            {
                if (_collider2D == null && !TryGetComponent(out _collider2D))
                {
                    PanicHelper.CheckAndPanicIfNull(_collider2D);
                }
                return _collider2D;
            }
        }

        protected EnemyPathFindingBehaviour PathFindingBehaviour
        {
            get
            {
                if (_pathFindingBehaviour == null && !TryGetComponent(out _pathFindingBehaviour))
                {
                    PanicHelper.CheckAndPanicIfNull(_pathFindingBehaviour);
                }
                return _pathFindingBehaviour;
            }
        }

        protected EnemyHealthBehaviour EnemyHealthBehaviour
        {
            get
            {
                if (_enemyHealthBehaviour == null && !TryGetComponent(out _enemyHealthBehaviour))
                {
                    PanicHelper.CheckAndPanicIfNull(_enemyHealthBehaviour);
                }
                return _enemyHealthBehaviour;
            }
        }

        protected Animator Animator
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponentInChildren<Animator>();
                    if (_animator == null)
                    {
                        PanicHelper.Panic(new Exception($"{nameof(Animator)} is not set"));
                    }
                }
                return _animator;
            }
        }

        public virtual void Initialize(
            PlayerPositionProvider playerPositionProvider,
            EnemyData enemyData,
            Action onDeath,
            Action onCleanUp)
        {
            PathFindingBehaviour.Initialize(playerPositionProvider);
            EnemyHealthBehaviour.Initialize(enemyData.Health);
            EnemyHealthBehaviour.SetDeathActions(
                onDeathDelay: 1f,
                onDeath: () =>
                {
                    Collider2D.enabled = false;
                    PathFindingBehaviour.Pause();
                    Animator.SetBool(AnimatorKey.IsDead, true);
                    onDeath?.Invoke();
                    HandleOnDeath();
                },
                onCleanUp: () =>
                {
                    onCleanUp?.Invoke();
                    HandleOnCleanUp();
                    Collider2D.enabled = true;
                    PathFindingBehaviour.CleanUp();
                    Animator.SetBool(AnimatorKey.IsDead, false);
                    ObjectPooler.Instance.ReturnToPool(enemyData.EnemyType, gameObject);
                });
        }
        
        protected abstract void HandleOnDeath();

        protected abstract void HandleOnCleanUp();
    }
}