using _WeaponMerge.Scripts.Managers.Data;

namespace _WeaponMerge.Scripts.Managers.Domain.UseCases
{
    public class GetWaveModeDataUseCase
    {
        private readonly WaveModeRepository _waveModeRepository;
        
        public GetWaveModeDataUseCase(WaveModeRepository waveModeRepository)
        {
            _waveModeRepository = waveModeRepository;
        }
        
        public WaveModeData Execute()
        {
            return _waveModeRepository.GetWaveModeState();
        }
    }
}