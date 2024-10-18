using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public class MoveInventoryItemUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public MoveInventoryItemUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public void Execute(int itemId, int toSlotIndex)
        { 
            _inventoryRepository.MoveItem(itemId, toSlotIndex);
        }
    }
}