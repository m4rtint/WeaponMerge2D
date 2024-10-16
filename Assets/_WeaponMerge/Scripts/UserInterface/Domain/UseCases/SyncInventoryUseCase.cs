using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain.UseCases
{
    public class SyncInventoryUseCase
    {
        private readonly IMergeRepository _repository;
        
        public SyncInventoryUseCase(IMergeRepository repository)
        {
            _repository = repository;
        }
        
        public void Execute()
        {
            _repository.SyncInventory();    
        }
    }
}