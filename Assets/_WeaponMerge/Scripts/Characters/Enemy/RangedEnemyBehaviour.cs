using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class RangedEnemyBehaviour : EnemyBehaviour
    {

        private EnemyRangedAttackBehaviour _enemyRangedAttackBehaviour = null;

        private void Awake()
        {
            _enemyRangedAttackBehaviour = GetComponent<EnemyRangedAttackBehaviour>();
            PanicHelper.CheckAndPanicIfNull(_enemyRangedAttackBehaviour);
        }
        
        public override void Initialize(
            PlayerPositionProvider playerPositionProvider,
            EnemyData data,
            Action onDeath, 
            Action onCleanUp)
        {
            base.Initialize(
                playerPositionProvider: playerPositionProvider,
                enemyData: data,
                onDeath: onDeath,
                onCleanUp: onCleanUp);
            _enemyRangedAttackBehaviour.Initialize(
                pausePathFindingAction: PathFindingBehaviour.Pause, 
                resumePathFindingAction: PathFindingBehaviour.Resume, 
                playerPositionProvider: playerPositionProvider,
                damage: data.Damage
            );        
        }

        protected override void HandleOnDeath()
        {
            _enemyRangedAttackBehaviour.StopAttack();
        }

        protected override void HandleOnCleanUp()
        {
            _enemyRangedAttackBehaviour.CleanUp();
        }
    }
}