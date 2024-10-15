using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.Presentation.Inventory;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Merge
{
    public struct MergeViewState
    {
        public SlotState PrimarySlot;
        public SlotState SecondarySlot;
        public SlotState[] InventorySlots;
    }

    public class MergeViewModel
    {
        private readonly SetMergeSlotUseCase _setMergeSlotUseCase;
        private readonly GetInventoryItemsUseCase _getInventoryItemsUseCase;

        private MergeViewState State
        {
            set => OnStateChanged?.Invoke(value);
        }

        public event Action<MergeViewState> OnStateChanged;

        public MergeViewModel(
            SetMergeSlotUseCase setMergeSlotUseCase,
            GetInventoryItemsUseCase getInventoryItemsUseCase)
        {
            _setMergeSlotUseCase = setMergeSlotUseCase;
            _getInventoryItemsUseCase = getInventoryItemsUseCase;
        }

        public void FetchItems()
        {
            var inventoryItems = _getInventoryItemsUseCase.Execute();
            State = MapInitialInventoryItemsToMergeViewState(inventoryItems);
        }

        private void MoveItem(int itemIndex, int toSlotIndex)
        {
            
        }

        private MergeViewState MapInitialInventoryItemsToMergeViewState(Item[] items)
        {
            var inventorySlots = new SlotState[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                inventorySlots[i] = new SlotState(
                    slotIndex: i,
                    itemId: item.Id,
                    itemImage: item.Image,
                    name: item.Name,
                    onMoveItem: MoveItem
                );
            }

            return new MergeViewState
            {
                PrimarySlot = SlotState.EmptyState(),
                SecondarySlot = SlotState.EmptyState(),
                InventorySlots = inventorySlots
            };
        }
    }
}