using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory
{
    public struct InventoryState
    {
        public SlotState[] InventoryItems;
        public SlotState[] EquipmentItems;
    }

    public struct SlotState
    {
        public int SlotIndex { get; }
        public int ItemId { get; }
        public Sprite ItemImage { get; }
        public string Name { get; }
        public Action<int, int> OnMoveItem { get; }
        public Action<Sprite> OnBeginDrag { get; }
        public Action OnDragging { get; }
        public Action<SlotView, bool> OnEndDrag { get; }
        
        public SlotState(
            int slotIndex,
            int itemId,
            Sprite itemImage,
            string name,
            Action<int, int> onMoveItem, 
            Action<Sprite> onBeginDrag,
            Action onDragging, 
            Action<SlotView, bool> onEndDrag)
        {
            SlotIndex = slotIndex;
            ItemId = itemId;
            ItemImage = itemImage;
            Name = name;
            OnMoveItem = onMoveItem;
            OnBeginDrag = onBeginDrag;
            OnDragging = onDragging;
            OnEndDrag = onEndDrag;
        }
    }
    
    public class InventoryViewModel
    {
        private readonly MoveInventoryItemUseCase _moveInventoryItemUseCase;
        private readonly GetInventoryItemsUseCase _getInventoryItemsUseCase;
        private readonly GetEquipmentItemsUseCase _getEquipmentItemsUseCase;
        private readonly IDragAndDrop _dragAndDrop;

        private InventoryState State
        {
            set => OnStateChanged?.Invoke(value);
        }
        
        public event Action<InventoryState> OnStateChanged;
        
        public InventoryViewModel(
            MoveInventoryItemUseCase moveInventoryItemUseCase, 
            GetInventoryItemsUseCase getInventoryItemsUseCase,
            GetEquipmentItemsUseCase getEquipmentItemsUseCase, 
            IDragAndDrop dragAndDrop)
        {
            _moveInventoryItemUseCase = moveInventoryItemUseCase;
            _getInventoryItemsUseCase = getInventoryItemsUseCase;
            _getEquipmentItemsUseCase = getEquipmentItemsUseCase;
            _dragAndDrop = dragAndDrop;
        }


        public void FetchItems()
        {
            var inventoryItems = _getInventoryItemsUseCase.Execute();
            var equipmentItems = _getEquipmentItemsUseCase.Execute();
            State = MapItemsToInventoryState(inventoryItems, equipmentItems);
        }
        
        private InventoryState MapItemsToInventoryState(Item[] inventoryItems, Item[] equipmentItems)
        {
            return new InventoryState
            {
                InventoryItems = MapToSlotsState(inventoryItems),
                EquipmentItems = MapToSlotsState(
                    equipmentItems, 
                    initialSlotIndex: inventoryItems.Length)
            };
        }
        
        private SlotState[] MapToSlotsState(Item[] items, int initialSlotIndex = 0)
        {
            var state = new SlotState[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                state[i] = new SlotState(
                    slotIndex: initialSlotIndex + i,
                    itemId: items[i]?.Id ?? -1,
                    itemImage: items[i]?.Sprite,
                    name: items[i]?.Name,
                    onMoveItem: MoveItem,
                    onBeginDrag: BeginDrag,
                    onDragging: Dragging,
                    onEndDrag: EndDrag
                );
            }

            return state;
        }
        
        
        #region SlotActions
        private void MoveItem(int itemId, int toSlotIndex)
        { 
            _moveInventoryItemUseCase.Execute(itemId, toSlotIndex);
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