using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.Characters.Players.Domain.UseCases
{
    public class SwitchEquippedWeaponUseCase
    {
        private readonly IEquipmentRepository _equipmentRepository;
        
        public SwitchEquippedWeaponUseCase(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        
        public Item Execute(bool isIncrement)
        {
            return _equipmentRepository.SwitchEquippedWeapon(isIncrement);
        }
    }
}