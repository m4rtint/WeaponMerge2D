using System.Threading.Tasks;
using _WeaponMerge.Scripts.UserInterface.WaveModeUI;
using _WeaponMerge.Tools;

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

        private WaveModeState State { get; set; }
        private int _roundNumber = 1;

        public WaveModeSystem(EnemySpawnerManager enemySpawnerManager)
        {
            _enemySpawnerManager = enemySpawnerManager;
            _enemySpawnerManager.OnClearAllEnemies += OnClearAllEnemies;
            State = WaveModeState.Transitioning;
        }

        public async void Start()
        {
            HUDWaveView.Instance.Set(new WaveModeData(waveNumber: _roundNumber));
            HUDWaveView.Instance.ShowAnnouncement();
            await Task.Delay(2000); // Delay for 2 seconds before starting
            _enemySpawnerManager.SetEnemiesToSpawn(BuildWave(1));
            State = WaveModeState.WaveInProgress;
            Logger.Log("Wave started!", LogKey.WaveMode);
        }

        private EnemyType[] BuildWave(int roundNumber)
        {
            return new EnemyType[] { EnemyType.Simple };
        }

        private void OnClearAllEnemies()
        {
            if (State == WaveModeState.WaveInProgress)
            {
                Logger.Log("Wave cleared!", LogKey.WaveMode);
                State = WaveModeState.Transitioning;
                _roundNumber++;
                HUDWaveView.Instance.Set(new WaveModeData(waveNumber: _roundNumber));
                HUDWaveView.Instance.ShowAnnouncement();
                Start();
            }
        }
    }
}