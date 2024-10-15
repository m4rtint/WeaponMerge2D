using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.UserInterface.Domain.Models;

namespace _WeaponMerge.Scripts.UserInterface.Domain.UseCases
{
    public class SetMergeSlotUseCase
    {
        private readonly IMergeRepository _repository;
        
        public SetMergeSlotUseCase(IMergeRepository repository)
        {
            _repository = repository;
        }
        
        public void Execute(int itemId, MergingSlot slot)
        {
            _repository.SetMergeItem(itemId, slot);
        }
    }
}