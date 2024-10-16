using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain.UseCases
{
    public class MergeItemsUseCase
    {
        private readonly IMergeRepository _repository;
        
        public MergeItemsUseCase(IMergeRepository repository)
        {
            _repository = repository;
        }
        
        public void Execute()
        {
            //LOGIC HERE TO MERGE REPOSITORY
        }
    }
}