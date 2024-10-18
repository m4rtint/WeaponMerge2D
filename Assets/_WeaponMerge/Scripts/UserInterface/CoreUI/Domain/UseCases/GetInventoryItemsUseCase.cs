using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public class GetInventoryItemsUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public GetInventoryItemsUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item[] Execute()
        {
            return _inventoryRepository.GetInventoryItems();
        }
    }
}