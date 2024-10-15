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
            _items = new Item[MAX_INVENTORY_ITEMS + MAX_EQUIPPED_ITEMS];
            MockItems();
        }

        private void MockItems()
        {
            var factory = new WeaponsFactory();
            for(int i = 0; i < MAX_INVENTORY_ITEMS; i++)
            {
                _items[i] = factory.CreateWeapon(WeaponType.Pistol);
            }

            _items[MAX_INVENTORY_ITEMS] = factory.CreateWeapon(WeaponType.Pistol);
        }
    }
}