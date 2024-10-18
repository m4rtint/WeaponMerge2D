using System.Threading.Tasks;
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

        public WaveModeSystem(
            EnemySpawnerManager enemySpawnerManager,
            IStoreWaveRoundNumberUseCase storeWaveRoundNumberUseCase)
        {
            _enemySpawnerManager = enemySpawnerManager;
            _enemySpawnerManager.OnClearAllEnemies += OnClearAllEnemies;
            _storeWaveRoundNumberUseCase = storeWaveRoundNumberUseCase;
            State = WaveModeState.Transitioning;
        }

        public async void Start()
        {
            HUDWaveView.Instance.ShowAnnouncement();
            await Task.Delay(3000); // Delay for 3 seconds before starting
            _enemySpawnerManager.SetEnemiesToSpawn(BuildWave(_roundNumber));
            State = WaveModeState.WaveInProgress;
            Logger.Log("Wave started!", LogKey.WaveMode);
        }

        private EnemyType[] BuildWave(int roundNumber)
        {
            // Adjustable difficulty parameters
            float simpleEnemyBase = 3f;  // Base number of simple enemies in round 1
            float shootingEnemyBase = 1f;  // Base number of shooting enemies in round 1
    
            // Difficulty scaling factor
            float difficultyScaling = 1.1f;

            // Cap the scaling factor to prevent excessive difficulty
            float scalingFactor = Mathf.Min(Mathf.Pow(difficultyScaling, roundNumber - 1), 10f);

            // Calculate number of enemies for this round
            int simpleEnemies = Mathf.RoundToInt(simpleEnemyBase * scalingFactor);
            int shootingEnemies = Mathf.RoundToInt(shootingEnemyBase * scalingFactor);

            // Add some random variation to the enemy counts (optional)
            simpleEnemies += Random.Range(-1, 2);  // Varies by -1 to +1 enemies
            shootingEnemies += Random.Range(0, 2);  // Varies by 0 to +1 enemies

            // Ensure minimum values for the number of enemies
            simpleEnemies = Mathf.Max(simpleEnemies, 1);
            shootingEnemies = Mathf.Max(shootingEnemies, 0);

            // Create an array to hold all enemies for this round
            EnemyType[] wave = new EnemyType[simpleEnemies + shootingEnemies];

            // Fill the wave array with simple enemies first
            for (int i = 0; i < simpleEnemies; i++)
            {
                wave[i] = EnemyType.Simple;
            }

            // Then fill the rest of the wave array with ranged (shooting) enemies
            for (int i = simpleEnemies; i < simpleEnemies + shootingEnemies; i++)
            {
                wave[i] = EnemyType.Ranged;
            }

            return wave;
        }


        private void OnClearAllEnemies()
        {
            if (State == WaveModeState.WaveInProgress)
            {
                Logger.Log("Wave cleared!", LogKey.WaveMode);
                State = WaveModeState.Transitioning;
                _roundNumber++;
                _storeWaveRoundNumberUseCase.Execute(_roundNumber);
                HUDWaveView.Instance.ShowAnnouncement();
                Start();
            }
        }
    }
}