using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain
{
    public class MoveItemUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public MoveItemUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item[] Execute(int itemId, int toSlotIndex)
        {
            return _inventoryRepository.MoveItem(itemId, toSlotIndex);
        }
    }
}