using _WeaponMerge.Scripts.Managers.Domain;

namespace _WeaponMerge.Scripts.Managers.Data
{
    public class WaveModeRepository
    {
        private static int _activeEnemies = 0;
        private static int _killedEnemies = 0;
        private static int _roundNumber = 1;
        
        public void StoreActiveEnemies(int activeEnemies)
        {
            _activeEnemies = activeEnemies;
        }
        
        public void StoreRoundNumber(int roundNumber)
        {
            _roundNumber = roundNumber;
        }
        
        public void StoreKilledEnemies(int killedEnemies)
        {
            _killedEnemies += killedEnemies;
        }
        
        public WaveModeData GetWaveModeState()
        {
            return new WaveModeData(_roundNumber, _activeEnemies, _killedEnemies);
        }
    }
}