using _WeaponMerge.Scripts.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.Domain.Models
{
    public struct MergeInventory
    {
        public MergingSlot PrimarySlot;
        public MergingSlot SecondarySlot;
        public Item[] InventoryItems;
    }
    
    public enum MergingSlot
    {
        Primary,
        Secondary
    }
}