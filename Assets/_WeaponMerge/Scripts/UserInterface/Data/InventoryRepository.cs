using System;
using System.Linq;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.Inventory
{
    public class InventoryRepository
    {
        private const int MAX_ITEMS = 16;
        private const int MAX_EQUIPPED_ITEMS = 4;

        private static Item[] _items;
        
        public InventoryRepository()
        {
            _items = new Item[MAX_ITEMS];
            _items[1] = new Weapon(
                1,
                "Pistol",
                0.5f,
                5f,
                10f,
                1,
                2f,
                10,
                0.5f,
                AmmoType.Pistol);

        }
        
        public Item[] MoveItem(int itemId, int toSlotIndex)
        {
            if (itemId == -1)
            {
                return _items;
            }
            
            var item = _items.FirstOrDefault(x => x?.Id == itemId);
            var isMoveValid = item == null || toSlotIndex < 0 || toSlotIndex >= _items.Length;
            if (isMoveValid)
            {
                // If item is not found or toSlotIndex is invalid, return the current items
                return _items;
            }

            var fromSlotIndex = Array.IndexOf(_items, item);
            if (fromSlotIndex == -1)
            {
                // If the item is not found in the inventory, return the current items
                return _items;
            }

            var toSlotItem = _items[toSlotIndex];
            _items[fromSlotIndex] = toSlotItem;
            _items[toSlotIndex] = item;

            return _items;
        }
        
        public Item[] GetItems()
        {
            return _items;
        }
    }
}