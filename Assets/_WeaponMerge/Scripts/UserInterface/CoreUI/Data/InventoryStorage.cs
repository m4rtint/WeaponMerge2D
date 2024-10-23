using System.Linq;
using _WeaponMerge.Scripts.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Data
{
    public class InventoryStorage
    {
        public const int MAX_INVENTORY_ITEMS = 16;
        public const int MAX_EQUIPPED_ITEMS = 4;
        public const int MAX_MERGE_SLOTS = 2;

        private static Item[] _allItems;
        private static int _equippedSlotIndex;

        public Item[] AllItems => _allItems;

        public int EquippedSlotIndex
        {
            get => _equippedSlotIndex;
            set => _equippedSlotIndex = value;
        }

        public Item[] InventoryItems => _allItems.Take(MAX_INVENTORY_ITEMS).ToArray();

        public InventoryStorage()
        {
            _allItems = new Item[MAX_INVENTORY_ITEMS + MAX_EQUIPPED_ITEMS];
        }

        public void CopyInventory(Item[] inventoryCopy)
        {
            for (int i = 0; i < inventoryCopy.Length - MAX_MERGE_SLOTS; i++)
            {
                _allItems[i] = inventoryCopy[i];
            }
        }
    }
}