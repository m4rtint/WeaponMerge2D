using System.Linq;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public class InventoryStorage
    {
        public const int MAX_INVENTORY_ITEMS = 16;
        public const int MAX_EQUIPPED_ITEMS = 4;

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
            MockItems();
        }

        public void CopyInventory(Item[] inventoryCopy)
        {
            for (int i = 0; i < inventoryCopy.Length; i++)
            {
                _allItems[i] = inventoryCopy[i];
            }
        }
  
        private void MockItems()
        {
            var factory = new WeaponsFactory();
            for(int i = 0; i < MAX_INVENTORY_ITEMS / 2; i++)
            {
                //Inventory
                _allItems[i] = factory.CreateWeapon(WeaponType.Pistol);
            }

            //Equipment
            _allItems[MAX_INVENTORY_ITEMS] = factory.CreateWeapon(WeaponType.Pistol);
        }
    }
}