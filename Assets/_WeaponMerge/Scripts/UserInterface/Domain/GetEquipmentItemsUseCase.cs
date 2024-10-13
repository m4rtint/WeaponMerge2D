using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain
{
    public class GetEquipmentItemsUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public GetEquipmentItemsUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item[] Execute()
        {
            return _inventoryRepository.GetEquipmentItems();
        }
    }
}