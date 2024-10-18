using _WeaponMerge.Scripts.Managers.Data;

namespace _WeaponMerge.Scripts.Managers.Domain.UseCases
{
    public class IncrementEnemiesKilledUseCase
    {
        private readonly WaveModeRepository _waveModeRepository;
        
        public IncrementEnemiesKilledUseCase(WaveModeRepository waveModeRepository)
        {
            _waveModeRepository = waveModeRepository;
        }
        
        public void Execute()
        {
            _waveModeRepository.IncrementEnemiesKilled();
        }
    }
}