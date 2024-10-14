using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.Characters.Players.Domain.UseCases
{
    public class GetEquippedWeaponUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public GetEquippedWeaponUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item Execute()
        {
            return _inventoryRepository.GetEquippedItem();
        }
    }
}