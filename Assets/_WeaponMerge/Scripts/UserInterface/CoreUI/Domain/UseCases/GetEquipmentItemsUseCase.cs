using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
{
    public class GetEquipmentItemsUseCase
    {
        private readonly IEquipmentRepository _equipmentRepository;
        
        public GetEquipmentItemsUseCase(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        
        public Item[] Execute()
        {
            return _equipmentRepository.GetEquipmentItems();
        }
    }
}