using System.Linq;
using _WeaponMerge.Scripts.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Data
{
    public interface IEquipmentRepository
    {
        Item GetEquippedItem();
        Item SwitchEquippedWeapon(bool isIncrement);
        Item[] GetEquipmentItems();
        int EquippedSlotIndex { get; }
    }
    
    public class EquipmentRepository: IEquipmentRepository
    {
        private readonly InventoryStorage _inventoryStorage;
        
        public EquipmentRepository(InventoryStorage inventoryStorage)
        {
            _inventoryStorage = inventoryStorage;
        }
        
        public Item GetEquippedItem()
        {
            return GetEquipmentItems()[_inventoryStorage.EquippedSlotIndex];
        }
        
        public Item SwitchEquippedWeapon(bool isIncrement)
        {
            if (isIncrement)
            {
                _inventoryStorage.EquippedSlotIndex = (_inventoryStorage.EquippedSlotIndex + 1) % InventoryStorage.MAX_EQUIPPED_ITEMS;
            }
            else
            {
                _inventoryStorage.EquippedSlotIndex = (_inventoryStorage.EquippedSlotIndex - 1 + InventoryStorage.MAX_EQUIPPED_ITEMS) % InventoryStorage.MAX_EQUIPPED_ITEMS;
            }

            return GetEquipmentItems()[_inventoryStorage.EquippedSlotIndex];
        }
        
        public Item[] GetEquipmentItems()
        {
            //TODO - all items is being over written. Need to fix this
            return _inventoryStorage.AllItems.Skip(InventoryStorage.MAX_INVENTORY_ITEMS).Take(InventoryStorage.MAX_EQUIPPED_ITEMS).ToArray();
        }

        public int EquippedSlotIndex => _inventoryStorage.EquippedSlotIndex;
    }
}