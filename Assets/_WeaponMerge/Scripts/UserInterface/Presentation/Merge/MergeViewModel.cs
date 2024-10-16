using System;
using _WeaponMerge.Scripts.UserInterface.Domain.Models;
using _WeaponMerge.Scripts.UserInterface.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.Presentation.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Merge
{
    public struct MergeViewState
    {
        public SlotState PrimarySlot;
        public SlotState SecondarySlot;
        public SlotState[] InventorySlots;
        public bool IsMergeButtonEnabled;
    }

    public class MergeViewModel
    {
        private readonly SyncInventoryUseCase _syncInventoryUseCase;
        private readonly MoveMergeItemUseCase _moveMergeItemUseCase;
        private readonly GetMergeInventoryUseCase _getMergeInventoryUseCase;
        private readonly MergeItemsUseCase _mergeItemsUseCase;
        
        private MergeViewState State
        {
            set => OnStateChanged?.Invoke(value);
        }

        public event Action<MergeViewState> OnStateChanged;

        public MergeViewModel(
            SyncInventoryUseCase syncInventoryUseCase,
            MergeItemsUseCase mergeItemsUseCase,
            MoveMergeItemUseCase moveMergeItemUseCase, 
            GetMergeInventoryUseCase getMergeInventoryUseCase)
        {
            _syncInventoryUseCase = syncInventoryUseCase;
            _mergeItemsUseCase = mergeItemsUseCase;
            _moveMergeItemUseCase = moveMergeItemUseCase;
            _getMergeInventoryUseCase = getMergeInventoryUseCase;
        }

        public void MergeItems()
        {
            _mergeItemsUseCase.Execute();
        }

        private void MoveInventoryItem(int itemIndex, int toSlotIndex)
        {
            _moveMergeItemUseCase.Execute(itemIndex, toSlotIndex);
            FetchItems();
        }

        public void FetchItems()
        {
            var mergeInventory = _getMergeInventoryUseCase.Execute();
            State = MapMergeViewState(mergeInventory);
        }


        public void SyncInventory()
        {
            _syncInventoryUseCase.Execute();
        }

        private MergeViewState MapMergeViewState(MergeInventory items)
        {
            var inventorySlots = new SlotState[items.InventoryItems.Length];
            int slotIndex;
            for (slotIndex = 0; slotIndex < items.InventoryItems.Length; slotIndex++)
            {
                var item = items.InventoryItems[slotIndex];
                inventorySlots[slotIndex] = new SlotState(
                    slotIndex: slotIndex,
                    itemId: item?.Id ?? -1,
                    itemImage: item?.Image,
                    name: item?.Name,
                    onMoveItem: MoveInventoryItem
                );
            }

            var primarySlotIndex = slotIndex;
            var primarySlot = new SlotState(
                slotIndex: primarySlotIndex,
                itemId: items.PrimarySlot?.Id ?? -1,
                itemImage: items.PrimarySlot?.Image,
                name: items.PrimarySlot?.Name,
                onMoveItem: MoveInventoryItem
            );

            var secondarySlotIndex = slotIndex + 1;
            var secondarySlot = new SlotState(
                slotIndex: secondarySlotIndex,
                itemId: items.SecondarySlot?.Id ?? -1,
                itemImage: items.SecondarySlot?.Image,
                name: items.SecondarySlot?.Name,
                onMoveItem: MoveInventoryItem
            );
            
            var isMergeButtonEnabled = items is { PrimarySlot: not null, SecondarySlot: not null };

            return new MergeViewState
            {
                PrimarySlot = primarySlot,
                SecondarySlot = secondarySlot,
                InventorySlots = inventorySlots,
                IsMergeButtonEnabled = isMergeButtonEnabled
            };
        }
    }
}