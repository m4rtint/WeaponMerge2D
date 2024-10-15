using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain.UseCases
{
    public class MoveItemUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public MoveItemUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public void Execute(int itemId, int toSlotIndex)
        { 
            _inventoryRepository.MoveItem(itemId, toSlotIndex);
        }
    }
}