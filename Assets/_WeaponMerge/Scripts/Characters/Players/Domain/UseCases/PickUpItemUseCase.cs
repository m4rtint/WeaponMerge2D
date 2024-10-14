using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.Characters.Players.Domain.UseCases
{
    public class PickUpItemUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;
        
        public PickUpItemUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public void Execute(Item item)
        {
            _inventoryRepository.AddItem(item);
        }
    }
}