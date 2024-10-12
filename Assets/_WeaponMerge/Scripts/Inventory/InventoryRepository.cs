namespace _WeaponMerge.Scripts.Inventory
{
    public class InventoryRepository
    {
        private const int MAX_ITEMS = 20;
        private const int MAX_EQUIPPED_ITEMS = 4;
        
        private static Item[] _items = new Item[MAX_ITEMS];
        
        public Item[] GetItems()
        {
            return _items;
        }
    }
}