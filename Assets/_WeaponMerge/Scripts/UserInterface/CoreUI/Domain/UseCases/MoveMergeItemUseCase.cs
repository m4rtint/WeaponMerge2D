using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public class MoveMergeItemUseCase
    {
        private readonly IMergeRepository _mergeRepository;
        
        public MoveMergeItemUseCase(IMergeRepository mergeRepository)
        {
            _mergeRepository = mergeRepository;
        }
        
        public void Execute(int itemId, int toSlotIndex)
        { 
            _mergeRepository.MoveItem(itemId, toSlotIndex);
        }
    }
}