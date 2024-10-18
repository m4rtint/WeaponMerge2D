namespace _WeaponMerge.Scripts.Managers.Domain
{
    public class WaveModeData
    {
        public int RoundNumber;
        public int ActiveEnemies;
        public int KilledEnemies;

        public WaveModeData(int roundNumber, int activeEnemies, int killedEnemies)
        {
            RoundNumber = roundNumber;
            ActiveEnemies = activeEnemies;
            KilledEnemies = killedEnemies;
        }
    }
}