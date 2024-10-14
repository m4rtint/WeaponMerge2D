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
        Item GetEquippedItem();
        Item SwitchEquippedWeapon(bool isIncrement);
        Item[] GetInventoryItems();
        Item[] GetEquipmentItems();
    }
    
    public class InventoryRepository: IInventoryRepository
    {
        private const int MAX_INVENTORY_ITEMS = 16;
        private const int MAX_EQUIPPED_ITEMS = 4;

        private static Item[] _items;
        private static int _equippedSlotIndex;
        
        public InventoryRepository()
        {
            _items = new Item[MAX_INVENTORY_ITEMS + MAX_EQUIPPED_ITEMS];
            _items[MAX_INVENTORY_ITEMS] = new Weapon(
                1,
                "Pistol",
                1f,
                5f,
                10f,
                1,
                2f,
                10,
                0.5f,
                AmmoType.Pistol);
        }
        
        public void AddItem(Item item)
        {
            if (TryAddToSlots(MAX_INVENTORY_ITEMS, _items.Length, item))
            {
                return;
            }
            TryAddToSlots(0, MAX_INVENTORY_ITEMS, item);
        }

        private bool TryAddToSlots(int start, int end, Item item)
        {
            for (int i = start; i < end; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
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
            
            var item = _items.FirstOrDefault(x => x?.Id == itemId);
            var isMoveValid = item == null || toSlotIndex < 0 || toSlotIndex >= _items.Length;
            if (isMoveValid)
            {
                // If item is not found or toSlotIndex is invalid, return the current items
                return;
            }

            var fromSlotIndex = Array.IndexOf(_items, item);
            if (fromSlotIndex == -1)
            {
                // If the item is not found in the inventory, return the current items
                return;
            }

            var toSlotItem = _items[toSlotIndex];
            _items[fromSlotIndex] = toSlotItem;
            _items[toSlotIndex] = item;
        }

        public Item GetEquippedItem()
        {
            return GetEquipmentItems()[_equippedSlotIndex];
        }

        public Item SwitchEquippedWeapon(bool isIncrement)
        {
            if (isIncrement)
            {
                _equippedSlotIndex = (_equippedSlotIndex + 1) % MAX_EQUIPPED_ITEMS;
            }
            else
            {
                _equippedSlotIndex = (_equippedSlotIndex - 1 + MAX_EQUIPPED_ITEMS) % MAX_EQUIPPED_ITEMS;
            }

            return GetEquipmentItems()[_equippedSlotIndex];
        }

        public Item[] GetInventoryItems()
        {
            return _items.Take(MAX_INVENTORY_ITEMS).ToArray();
        }
        
        public Item[] GetEquipmentItems()
        {
            return _items.Skip(MAX_INVENTORY_ITEMS).Take(MAX_EQUIPPED_ITEMS).ToArray();
        }
    }
}