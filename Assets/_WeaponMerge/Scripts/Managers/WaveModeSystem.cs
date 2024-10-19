using _WeaponMerge.Scripts.Characters.Enemy.Domain.Model;
using _WeaponMerge.Scripts.Managers.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.WaveModeUI;
using _WeaponMerge.Tools;
using UnityEngine;
using Logger = _WeaponMerge.Tools.Logger;

namespace _WeaponMerge.Scripts.Managers
{
    public enum WaveModeState
    {
        Transitioning,
        WaveInProgress,
    }

    public class WaveModeSystem
    {
        private readonly EnemySpawnerManager _enemySpawnerManager;
        private readonly IStoreWaveRoundNumberUseCase _storeWaveRoundNumberUseCase;

        private WaveModeState State { get; set; }
        private int _roundNumber = 1;
        private readonly IRandomness _randomness;

        public WaveModeSystem(
            EnemySpawnerManager enemySpawnerManager,
            IStoreWaveRoundNumberUseCase storeWaveRoundNumberUseCase)
        {
            _enemySpawnerManager = enemySpawnerManager;
            _enemySpawnerManager.OnClearAllEnemies += OnClearAllEnemies;
            _storeWaveRoundNumberUseCase = storeWaveRoundNumberUseCase;
            State = WaveModeState.Transitioning;
            _randomness = new Randomness();
        }

        public void Start()
        {
            const float delay = 3f;
            Logger.Log($"Transition {delay} second delay", LogKey.WaveMode);
            HUDWaveView.Instance.ShowAnnouncement(
                delayAnnouncement: delay,
                onAnnouncementComplete: () =>
            {
                _enemySpawnerManager.SetEnemiesToSpawn(BuildWave(_roundNumber));
                State = WaveModeState.WaveInProgress;
                Logger.Log($"Wave {_roundNumber} started! Spawn Started", LogKey.WaveMode);
            });
        }

        private EnemyData[] BuildWave(int roundNumber)
        {
            var scalingFactor = 1.3f;
            int meleeHealth = CalculateEnemyStat(20, scalingFactor, roundNumber);
            int rangedHealth = CalculateEnemyStat(15, scalingFactor, roundNumber);
            int meleeDamage = CalculateEnemyStat(10, scalingFactor, roundNumber);
            int rangedDamage = CalculateEnemyStat(12, scalingFactor, roundNumber);

            int simpleEnemies = CalculateEnemyCount(3f, scalingFactor, roundNumber);
            int shootingEnemies = CalculateEnemyCount(1f, scalingFactor, roundNumber);

            return CreateWave(simpleEnemies, shootingEnemies, meleeHealth, rangedHealth, meleeDamage, rangedDamage);
        }

        private int CalculateEnemyStat(int baseStat, float scalingFactor, int roundNumber)
        {
            return Mathf.RoundToInt(baseStat * Mathf.Pow(scalingFactor, roundNumber - 1));
        }

        private int CalculateEnemyCount(float baseCount, float scalingFactor, int roundNumber)
        {
            int count = Mathf.RoundToInt(baseCount * Mathf.Pow(scalingFactor, roundNumber - 1));
            count += _randomness.Range(-1, 2);  // Add some random variation
            return Mathf.Max(count, 1);    // Ensure minimum value
        }

        private EnemyData[] CreateWave(int simpleEnemies, int shootingEnemies, int meleeHealth, int rangedHealth, int meleeDamage, int rangedDamage)
        {
            EnemyData[] wave = new EnemyData[simpleEnemies + shootingEnemies];

            for (int i = 0; i < simpleEnemies; i++)
            {
                wave[i] = new EnemyData
                {
                    EnemyType = EnemyType.Simple,
                    Health = meleeHealth,
                    Damage = meleeDamage
                };
            }

            for (int i = simpleEnemies; i < simpleEnemies + shootingEnemies; i++)
            {
                wave[i] = new EnemyData
                {
                    EnemyType = EnemyType.Ranged,
                    Health = rangedHealth,
                    Damage = rangedDamage
                };
            }

            return wave;
        }

        private void OnClearAllEnemies()
        {
            if (State == WaveModeState.WaveInProgress)
            {
                Logger.Log($"Wave {_roundNumber} cleared!", LogKey.WaveMode);
                State = WaveModeState.Transitioning;
                _roundNumber++;
                _storeWaveRoundNumberUseCase.Execute(_roundNumber);
                Start();
            }
        }

        public void CleanUp()
        {
            _roundNumber = 1;
            State = WaveModeState.Transitioning;
        }
    }
}