using System;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.Models;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Merge
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
        private readonly MoveMergeItemUseCase _moveMergeItemUseCase;
        private readonly GetMergeInventoryUseCase _getMergeInventoryUseCase;
        private readonly MergeItemsUseCase _mergeItemsUseCase;
        
        private MergeViewState State
        {
            set => OnStateChanged?.Invoke(value);
        }

        public event Action<MergeViewState> OnStateChanged;

        public MergeViewModel(
            MergeItemsUseCase mergeItemsUseCase,
            MoveMergeItemUseCase moveMergeItemUseCase, 
            GetMergeInventoryUseCase getMergeInventoryUseCase)
        {
            _mergeItemsUseCase = mergeItemsUseCase;
            _moveMergeItemUseCase = moveMergeItemUseCase;
            _getMergeInventoryUseCase = getMergeInventoryUseCase;
        }

        public void MergeItems()
        {
            _mergeItemsUseCase.Execute();
            FetchItems();
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
                    itemImage: item?.Sprite,
                    name: item?.Name,
                    onMoveItem: MoveInventoryItem,
                    onBeginDrag: null,
                    onDragging: null,
                    onEndDrag: null
                );
            }

            var primarySlotIndex = slotIndex;
            var primarySlot = new SlotState(
                slotIndex: primarySlotIndex,
                itemId: items.PrimarySlot?.Id ?? -1,
                itemImage: items.PrimarySlot?.Sprite,
                name: items.PrimarySlot?.Name,
                onMoveItem: MoveInventoryItem,
                onBeginDrag: null,
                onDragging: null,
                onEndDrag: null
            );

            var secondarySlotIndex = slotIndex + 1;
            var secondarySlot = new SlotState(
                slotIndex: secondarySlotIndex,
                itemId: items.SecondarySlot?.Id ?? -1,
                itemImage: items.SecondarySlot?.Sprite,
                name: items.SecondarySlot?.Name,
                onMoveItem: MoveInventoryItem,
                onBeginDrag: null,
                onDragging: null,
                onEndDrag: null
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