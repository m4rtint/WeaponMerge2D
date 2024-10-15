using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain.Models;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.UserInterface.Data
{
    public interface IMergeRepository
    {
        void SetMergeItem(int itemId, MergingSlot slot);
        void AddMergedItem(Item item);
    }
    
    public class MergeRepository: IMergeRepository
    {
        private const int MAX_ITEMS_TO_MERGE = 2;
        
        private readonly InventoryStorage _storage;
        private Item[] _itemsToMerge = new Item[MAX_ITEMS_TO_MERGE];

        public MergeRepository(InventoryStorage storage)
        {
            _storage = storage;
        }

        public void SetMergeItem(int itemId, MergingSlot slot)
        {
            PanicHelper.Panic(new Exception("Not implemented"));
        }

        public void AddMergedItem(Item item)
        {
            PanicHelper.Panic(new Exception("Not implemented"));
        }
    }
}