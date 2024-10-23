using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public class GetEquippedSlotIndexUseCase
    {
        private readonly EquipmentRepository _equipmentRepository;
        
        public GetEquippedSlotIndexUseCase(EquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        
        public int Execute()
        {
            return _equipmentRepository.EquippedSlotIndex;
        }
    }
}