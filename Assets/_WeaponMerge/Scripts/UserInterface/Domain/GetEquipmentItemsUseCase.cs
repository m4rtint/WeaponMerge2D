using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;

namespace _WeaponMerge.Scripts.UserInterface.Domain
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