using System;
using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class SimpleEnemyBehaviour : EnemyBehaviour
    {
        [SerializeField] AudioClip[] _deathAudioClips = null;
        private SimpleEnemyAttackBehaviour _simpleEnemyAttackBehaviour = null;
        private IEnemyFeedbackEffect _enemyFeedbackEffect;

        private void Awake()
        {
            _enemyFeedbackEffect = GetComponent<EnemyFeedbackEffectBehaviour>();
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
            _enemyFeedbackEffect.DeathAudioEffects(
                _deathAudioClips,
                volume: 0.5f,
                doRandomPitch: true);
        }


        protected override void HandleOnCleanUp()
        {
            _simpleEnemyAttackBehaviour.CleanUp();
        }
    }
}