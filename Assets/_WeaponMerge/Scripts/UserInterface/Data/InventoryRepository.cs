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
        }
        
        public void AddItem(Item item)
        {
            if (TryAddToSlots(InventoryStorage.MAX_INVENTORY_ITEMS, _inventoryStorage.AllItems.Length, item))
            {
                return;
            }
            TryAddToSlots(0, InventoryStorage.MAX_INVENTORY_ITEMS, item);
        }

        private bool TryAddToSlots(int start, int end, Item item)
        {
            for (int i = start; i < end; i++)
            {
                if (_inventoryStorage.AllItems[i] == null)
                {
                    _inventoryStorage.AllItems[i] = item;
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
            
            var item = _inventoryStorage.AllItems.FirstOrDefault(x => x?.Id == itemId);
            var isMoveValid = item == null || toSlotIndex < 0 || toSlotIndex >= _inventoryStorage.AllItems.Length;
            if (isMoveValid)
            {
                // If item is not found or toSlotIndex is invalid, return the current items
                return;
            }

            var fromSlotIndex = Array.IndexOf(_inventoryStorage.AllItems, item);
            if (fromSlotIndex == -1)
            {
                // If the item is not found in the inventory, return the current items
                return;
            }

            var toSlotItem = _inventoryStorage.AllItems[toSlotIndex];
            _inventoryStorage.AllItems[fromSlotIndex] = toSlotItem;
            _inventoryStorage.AllItems[toSlotIndex] = item;
        }

        public Item[] GetInventoryItems()
        {
            return _inventoryStorage.AllItems.Take(InventoryStorage.MAX_INVENTORY_ITEMS).ToArray();
        }

        public Item GetInventoryItem(int itemId)
        {
            return _inventoryStorage.AllItems.FirstOrDefault(x => x?.Id == itemId);
        }
    }
}