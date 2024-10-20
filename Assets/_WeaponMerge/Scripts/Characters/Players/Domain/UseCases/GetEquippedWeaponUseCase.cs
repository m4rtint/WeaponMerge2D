using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.Characters.Players.Domain.UseCases
{
    public class GetEquippedWeaponUseCase
    {
        private readonly IEquipmentRepository _equipmentRepository;
        
        public GetEquippedWeaponUseCase(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        
        public Item Execute()
        {
            return _equipmentRepository.GetEquippedItem();
        }
    }
}