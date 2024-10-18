using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.Models;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public class GetMergeInventoryUseCase
    {
        private readonly IMergeRepository _mergeRepository;
        
        public GetMergeInventoryUseCase(IMergeRepository mergeRepository)
        {
            _mergeRepository = mergeRepository;
        }
        
        public MergeInventory Execute()
        {
            return _mergeRepository.GetMergeInventory();
        }
    }
}