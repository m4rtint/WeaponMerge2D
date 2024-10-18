using _WeaponMerge.Scripts.Managers.Data;

namespace _WeaponMerge.Scripts.Managers.Domain.UseCases
{
    public interface IStoreWaveRoundNumberUseCase
    {
        void Execute(int roundNumber);
    }

    public class StoreWaveRoundNumberUseCase : IStoreWaveRoundNumberUseCase
    {
        private readonly WaveModeRepository _repository;

        public StoreWaveRoundNumberUseCase(WaveModeRepository repository)
        {
            _repository = repository;
        }

        public void Execute(int roundNumber)
        {
            _repository.StoreRoundNumber(roundNumber);
        }
    }
}