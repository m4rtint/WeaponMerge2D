using _WeaponMerge.Scripts.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.Domain.Models
{
    public struct MergeInventory
    {
        public Item PrimarySlot;
        public Item SecondarySlot;
        public Item[] InventoryItems;
        
        public MergeInventory(
            Item primarySlot, 
            Item secondarySlot, 
            Item[] inventoryItems)
        {
            PrimarySlot = primarySlot;
            SecondarySlot = secondarySlot;
            InventoryItems = inventoryItems;
        }
    }
}