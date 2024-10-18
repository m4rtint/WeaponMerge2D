using _WeaponMerge.Scripts.Managers.Data;

namespace _WeaponMerge.Scripts.Managers.Domain.UseCases
{
    public interface IStoreActiveEnemiesUseCase
    {
        void Execute(int activeEnemies);
    }
    
    public class StoreWaveActiveEnemiesUseCase: IStoreActiveEnemiesUseCase
    {
        private readonly WaveModeRepository _repository;
        
        public StoreWaveActiveEnemiesUseCase(WaveModeRepository repository)
        {
            _repository = repository;
        }
        
        public void Execute(int activeEnemies)
        {
            _repository.StoreActiveEnemies(activeEnemies);
        }
    }
}