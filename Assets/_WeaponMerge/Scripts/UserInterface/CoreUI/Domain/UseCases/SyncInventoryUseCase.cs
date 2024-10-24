using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public enum SyncType
    {
        ToInventory,
        ToMerge
    }
    public class SyncInventoryUseCase
    {
        private readonly IMergeRepository _repository;
        
        public SyncInventoryUseCase(IMergeRepository repository)
        {
            _repository = repository;
        }
        
        public void Execute(SyncType type)
        {
            _repository.SyncInventory(type);    
        }
    }
}