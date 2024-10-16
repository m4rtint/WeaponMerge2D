using System;
using System.Linq;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain.Models;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public interface IMergeRepository
    {
        void AddMergedItem(Item item);
        void MoveItem(int itemId, int toSlotIndex);
        MergeInventory GetMergeInventory();
    }
    
    public class MergeRepository: IMergeRepository
    {
        private const int MAX_ITEMS_TO_MERGE = 2;
        
        // Inventory
        private readonly InventoryStorage _storage;
        
        // Merge
        private readonly Item[] _inventoryToMerge;
        
        public MergeRepository(InventoryStorage storage)
        {
            _storage = storage;
            _inventoryToMerge = new Item[_storage.InventoryItems.Length + MAX_ITEMS_TO_MERGE];
            Array.Copy(_storage.InventoryItems, _inventoryToMerge, _storage.InventoryItems.Length);
        }

        public void AddMergedItem(Item item)
        {
            PanicHelper.Panic(new Exception("Not implemented"));
        }

        public void MoveItem(int itemId, int toSlotIndex)
        {
            if (itemId == -1)
            {
                return;
            }
            
            var item = _inventoryToMerge.FirstOrDefault(x => x?.Id == itemId);
            var isMoveInvalid = item == null || toSlotIndex < 0 || toSlotIndex >= _inventoryToMerge.Length;
            if (isMoveInvalid)
            {
                // If item is not found or toSlotIndex is invalid, return the current items
                return;
            }

            var fromSlotIndex = Array.IndexOf(_inventoryToMerge, item);
            if (fromSlotIndex == -1)
            {
                // If the item is not found in the inventory, return the current items
                return;
            }

            var toSlotItem = _inventoryToMerge[toSlotIndex];
            _inventoryToMerge[fromSlotIndex] = toSlotItem;
            _inventoryToMerge[toSlotIndex] = item;
        }

        public MergeInventory GetMergeInventory()
        {
            var inventoryItems = _inventoryToMerge.Take(_inventoryToMerge.Length - MAX_ITEMS_TO_MERGE).ToArray();
            return new MergeInventory
            {
                InventoryItems = inventoryItems,
                PrimarySlot = _inventoryToMerge[^MAX_ITEMS_TO_MERGE],
                SecondarySlot = _inventoryToMerge[_inventoryToMerge.Length -MAX_ITEMS_TO_MERGE + 1]
            };
        }
    }
}