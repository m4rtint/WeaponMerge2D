using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.Characters.Players.Domain.UseCases
{
    public class SwitchEquippedWeaponUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public SwitchEquippedWeaponUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item Execute(bool isIncrement)
        {
            return _inventoryRepository.SwitchEquippedWeapon(isIncrement);
        }
    }
}