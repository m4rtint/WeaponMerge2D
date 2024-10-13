using System;
using System.Linq;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public class InventoryRepository
    {
        private const int MAX_INVENTORY_ITEMS = 16;
        private const int MAX_EQUIPPED_ITEMS = 4;

        private static Item[] _items;
        
        public InventoryRepository()
        {
            _items = new Item[MAX_INVENTORY_ITEMS + MAX_EQUIPPED_ITEMS];
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
            _items[MAX_INVENTORY_ITEMS + 2] = new Weapon(
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