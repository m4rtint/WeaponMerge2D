using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain.UseCases
{
    public class GetInventoryItemUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public GetInventoryItemUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item Execute(int itemId)
        {
            return _inventoryRepository.GetInventoryItem(itemId);
        }
    }
}