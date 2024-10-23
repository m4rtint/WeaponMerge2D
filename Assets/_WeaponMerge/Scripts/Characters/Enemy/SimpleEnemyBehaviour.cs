using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class SimpleEnemyBehaviour : EnemyBehaviour
    {
        private SimpleEnemyAttackBehaviour _simpleEnemyAttackBehaviour = null;

        private void Awake()
        {
            _simpleEnemyAttackBehaviour = GetComponent<SimpleEnemyAttackBehaviour>();
            PanicHelper.CheckAndPanicIfNull(_simpleEnemyAttackBehaviour);
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
            _simpleEnemyAttackBehaviour.Initialize(damage: data.Damage);
        }

        protected override void HandleOnDeath()
        {
            _simpleEnemyAttackBehaviour.StopAttack();
        }

        protected override void HandleOnCleanUp()
        {
            _simpleEnemyAttackBehaviour.CleanUp();
        }
    }
}