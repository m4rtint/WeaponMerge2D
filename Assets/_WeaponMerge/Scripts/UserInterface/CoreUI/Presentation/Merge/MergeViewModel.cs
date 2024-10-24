using System;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.Models;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory;
using UnityEngine;

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
        private readonly SyncInventoryUseCase _syncInventoryUseCase;
        private readonly IDragAndDrop _dragAndDrop;

        private MergeViewState State
        {
            set => OnStateChanged?.Invoke(value);
        }

        public event Action<MergeViewState> OnStateChanged;

        public MergeViewModel(
            MergeItemsUseCase mergeItemsUseCase,
            MoveMergeItemUseCase moveMergeItemUseCase, 
            GetMergeInventoryUseCase getMergeInventoryUseCase, 
            SyncInventoryUseCase syncInventoryUseCase,
            IDragAndDrop dragAndDrop)
        {
            _mergeItemsUseCase = mergeItemsUseCase;
            _moveMergeItemUseCase = moveMergeItemUseCase;
            _getMergeInventoryUseCase = getMergeInventoryUseCase;
            _syncInventoryUseCase = syncInventoryUseCase;
            _dragAndDrop = dragAndDrop;
        }

        public void MergeItems()
        {
            _mergeItemsUseCase.Execute();
            FetchItems();
        }

        public void LoadMergeItems()
        {
            _syncInventoryUseCase.Execute(SyncType.ToMerge);
            FetchItems();
        }

        public void SyncToInventory()
        {
            _syncInventoryUseCase.Execute(SyncType.ToInventory);
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
                    onBeginDrag: BeginDrag,
                    onDragging: Dragging,
                    onEndDrag: EndDrag
                );
            }

            var primarySlotIndex = slotIndex;
            var primarySlot = new SlotState(
                slotIndex: primarySlotIndex,
                itemId: items.PrimarySlot?.Id ?? -1,
                itemImage: items.PrimarySlot?.Sprite,
                name: items.PrimarySlot?.Name,
                onMoveItem: MoveInventoryItem,
                onBeginDrag: BeginDrag,
                onDragging: Dragging,
                onEndDrag: EndDrag
            );

            var secondarySlotIndex = slotIndex + 1;
            var secondarySlot = new SlotState(
                slotIndex: secondarySlotIndex,
                itemId: items.SecondarySlot?.Id ?? -1,
                itemImage: items.SecondarySlot?.Sprite,
                name: items.SecondarySlot?.Name,
                onMoveItem: MoveInventoryItem,
                onBeginDrag: BeginDrag,
                onDragging: Dragging,
                onEndDrag: EndDrag
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
        
        #region SlotActions
        private void MoveInventoryItem(int itemIndex, int toSlotIndex)
        {
            _moveMergeItemUseCase.Execute(itemIndex, toSlotIndex);
            FetchItems();
        }

        private void BeginDrag(Sprite sprite)
        {
            _dragAndDrop.OnBeginDrag(sprite);
        }

        private void Dragging()
        {
            _dragAndDrop.Dragging();
        }

        private void EndDrag(SlotView fromSlotView, bool isOverSlot)
        {
            _dragAndDrop.OnEndDrag(fromSlotView, isOverSlot);
        }
        #endregion
    }
}