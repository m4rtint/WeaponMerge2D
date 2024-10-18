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

        public WaveModeState State { get; set; }
        public int _roundNumber = 0;

        public WaveModeSystem(EnemySpawnerManager enemySpawnerManager)
        {
            _enemySpawnerManager = enemySpawnerManager;
            _enemySpawnerManager.OnClearAllEnemies += OnClearAllEnemies;
            State = WaveModeState.Transitioning;
        }

        public void Start()
        {
            _enemySpawnerManager.SetEnemiesToSpawn(BuildWave(1));
            State = WaveModeState.WaveInProgress;
        }

        private EnemyType[] BuildWave(int roundNumber)
        {
            return new EnemyType[] { EnemyType.Simple };
        }

        private void OnClearAllEnemies()
        {
            State = WaveModeState.Transitioning;
            _roundNumber++;
            //TODO - HUD Update
        }
    }
}