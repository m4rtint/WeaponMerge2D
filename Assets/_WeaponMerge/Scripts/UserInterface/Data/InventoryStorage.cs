using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public class InventoryStorage
    {
        public const int MAX_INVENTORY_ITEMS = 16;
        public const int MAX_EQUIPPED_ITEMS = 4;

        private static Item[] _items;
        private static int _equippedSlotIndex;

        public Item[] Items
        {
            get => _items;
            set => _items = value;
        }

        public int EquippedSlotIndex
        {
            get => _equippedSlotIndex;
            set => _equippedSlotIndex = value;
        }
        
        public InventoryStorage()
        {
            Items = new Item[MAX_INVENTORY_ITEMS + MAX_EQUIPPED_ITEMS];
            Items[MAX_INVENTORY_ITEMS] = new Weapon(
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
    }
}