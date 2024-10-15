using System;
using System.Linq;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public interface IInventoryRepository
    {
        void MoveItem(int itemId, int toSlotIndex);
        void AddItem(Item item);
        Item[] GetInventoryItems();

        Item GetInventoryItem(int itemId);
    }
    
    public class InventoryRepository: IInventoryRepository
    {
        private readonly InventoryStorage _inventoryStorage;
        
        public InventoryRepository(InventoryStorage inventoryStorage)
        {
            _inventoryStorage = inventoryStorage;
            _inventoryStorage.Items = new Item[InventoryStorage.MAX_INVENTORY_ITEMS + InventoryStorage.MAX_EQUIPPED_ITEMS];
            _inventoryStorage.Items[InventoryStorage.MAX_INVENTORY_ITEMS] = new Weapon(
                1,
                "Pistol",
                0.1f,
                30f,
                10f,
                4,
                0.5f,
                10,
                0.5f,
                AmmoType.Pistol);
        }
        
        public void AddItem(Item item)
        {
            if (TryAddToSlots(InventoryStorage.MAX_INVENTORY_ITEMS, _inventoryStorage.Items.Length, item))
            {
                return;
            }
            TryAddToSlots(0, InventoryStorage.MAX_INVENTORY_ITEMS, item);
        }

        private bool TryAddToSlots(int start, int end, Item item)
        {
            for (int i = start; i < end; i++)
            {
                if (_inventoryStorage.Items[i] == null)
                {
                    _inventoryStorage.Items[i] = item;
                    return true;
                }
            }
            return false;
        }
        
        public void MoveItem(int itemId, int toSlotIndex)
        {
            if (itemId == -1)
            {
                return;
            }
            
            var item = _inventoryStorage.Items.FirstOrDefault(x => x?.Id == itemId);
            var isMoveValid = item == null || toSlotIndex < 0 || toSlotIndex >= _inventoryStorage.Items.Length;
            if (isMoveValid)
            {
                // If item is not found or toSlotIndex is invalid, return the current items
                return;
            }

            var fromSlotIndex = Array.IndexOf(_inventoryStorage.Items, item);
            if (fromSlotIndex == -1)
            {
                // If the item is not found in the inventory, return the current items
                return;
            }

            var toSlotItem = _inventoryStorage.Items[toSlotIndex];
            _inventoryStorage.Items[fromSlotIndex] = toSlotItem;
            _inventoryStorage.Items[toSlotIndex] = item;
        }

        public Item[] GetInventoryItems()
        {
            return _inventoryStorage.Items.Take(InventoryStorage.MAX_INVENTORY_ITEMS).ToArray();
        }

        public Item GetInventoryItem(int itemId)
        {
            return _inventoryStorage.Items.FirstOrDefault(x => x?.Id == itemId);
        }
    }
}