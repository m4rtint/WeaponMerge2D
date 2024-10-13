using System;
using System.Linq;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public class InventoryRepository
    {
        private static Item[] _items;
        
        public InventoryRepository(int maxInventorySpace)
        {
            _items = new Item[maxInventorySpace];
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
            //TODO Create anoher pistol
            _items[maxInventorySpace -2] = new Weapon(
                2,
                "Rifle",
                0.1f,
                1f,
                20f,
                1,
                2f,
                20,
                0.5f,
                AmmoType.Rifle);

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