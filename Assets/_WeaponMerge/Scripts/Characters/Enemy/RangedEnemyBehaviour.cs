using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class RangedEnemyBehaviour : EnemyBehaviour
    {
        [SerializeField] AudioClip _attackAudioClip = null;
        [SerializeField] AudioClip[] _deathAudioClips = null;
        private IEnemyFeedbackEffect _enemyFeedbackEffect;
        private EnemyRangedAttackBehaviour _enemyRangedAttackBehaviour = null;

        private void Awake()
        {
            _enemyFeedbackEffect = GetComponent<EnemyFeedbackEffectBehaviour>();
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
            _enemyFeedbackEffect?.DeathAudioEffects(_deathAudioClips, volume: 0.75f);
            _enemyRangedAttackBehaviour.StopAttack();
        }

        protected override void HandleOnCleanUp()
        {
            _enemyRangedAttackBehaviour.CleanUp();
        }
    }
}