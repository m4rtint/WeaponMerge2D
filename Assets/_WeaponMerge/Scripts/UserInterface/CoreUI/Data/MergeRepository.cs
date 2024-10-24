using System;
using System.Linq;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.Models;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Data
{
    public interface IMergeRepository
    {
        void MoveItem(int itemId, int toSlotIndex);
        MergeInventory GetMergeInventory();
        (Item, Item) GetMergingItems();
        void AddMergedItemToInventory(Item item);
        void SyncInventory();
    }
    
    public class MergeRepository: IMergeRepository
    {
        // Inventory
        private readonly InventoryStorage _storage;
        
        // Merge
        private readonly Item[] _inventoryToMerge;
        
        public MergeRepository(InventoryStorage storage)
        {
            _storage = storage;
            _inventoryToMerge = new Item[_storage.InventoryItems.Length + InventoryStorage.MAX_MERGE_SLOTS];
        }

        public (Item, Item) GetMergingItems()
        {
            return (_inventoryToMerge[^InventoryStorage.MAX_MERGE_SLOTS], _inventoryToMerge[^1]);
        }

        public void AddMergedItemToInventory(Item item)
        {
            for (int i = 0; i < _inventoryToMerge.Length; i++)
            {
                if (_inventoryToMerge[i] == null)
                {
                    _inventoryToMerge[i] = item;
                    break;
                }
            }
            
            _inventoryToMerge[^InventoryStorage.MAX_MERGE_SLOTS] = null;
            _inventoryToMerge[^1] = null;
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
            Array.Copy(_storage.InventoryItems, _inventoryToMerge, InventoryStorage.MAX_INVENTORY_ITEMS);
            var inventoryItems = _inventoryToMerge.Take(_inventoryToMerge.Length - InventoryStorage.MAX_MERGE_SLOTS).ToArray();
            return new MergeInventory
            {
                InventoryItems = inventoryItems,
                PrimarySlot = _inventoryToMerge[^InventoryStorage.MAX_MERGE_SLOTS],
                SecondarySlot = _inventoryToMerge[_inventoryToMerge.Length - InventoryStorage.MAX_MERGE_SLOTS + 1]
            };
        }
        
        public void SyncInventory()
        {
            var primaryMergeSlot = _inventoryToMerge[^InventoryStorage.MAX_MERGE_SLOTS];
            var secondaryMergeSlot = _inventoryToMerge[^1];

            AddItemToFirstEmptySlot(primaryMergeSlot);
            AddItemToFirstEmptySlot(secondaryMergeSlot);

            _storage.CopyInventory(_inventoryToMerge);
        }

        private void AddItemToFirstEmptySlot(Item item)
        {
            for (int index = 0; index < _inventoryToMerge.Length; index++)
            {
                if (_inventoryToMerge[index] == null)
                {
                    _inventoryToMerge[index] = item;
                    break;
                }
            }
        }
    }
}