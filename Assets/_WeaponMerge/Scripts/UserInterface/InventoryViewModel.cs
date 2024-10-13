using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface
{
    public struct InventoryState
    {
        public InventorySlotState[] InventoryItems;
        public InventorySlotState[] EquipmentItems;
    }

    public struct InventorySlotState
    {
        public readonly int SlotIndex;
        public readonly int ItemId;
        public readonly Image ItemImage;
        public readonly string Name;
        public readonly Action<int, int> OnMoveItem;
        
        public InventorySlotState(int slotIndex, int itemId, Image itemImage, string name, Action<int, int> onMoveItem)
        {
            SlotIndex = slotIndex;
            ItemId = itemId;
            ItemImage = itemImage;
            Name = name;
            OnMoveItem = onMoveItem;
        }

        public InventorySlotState InitialState()
        {
            return new InventorySlotState(
                slotIndex: -1,
                itemId: -1,
                itemImage: null,
                name: null,
                onMoveItem: null
            );
        }
    }
    
    public class InventoryViewModel
    {
        private readonly MoveItemUseCase _moveItemUseCase;
        private readonly GetInventoryItemsUseCase _getInventoryItemsUseCase;
        private readonly GetEquipmentItemsUseCase _getEquipmentItemsUseCase;
        private InventoryState _state;

        private InventoryState State
        {
            get => _state;
            set
            {
                _state = value;
                OnStateChanged?.Invoke(value);
            }
        }
        
        public event Action<InventoryState> OnStateChanged;
        
        public InventoryViewModel(
            MoveItemUseCase moveItemUseCase, 
            GetInventoryItemsUseCase getInventoryItemsUseCase,
            GetEquipmentItemsUseCase getEquipmentItemsUseCase)
        {
            _moveItemUseCase = moveItemUseCase;
            _getInventoryItemsUseCase = getInventoryItemsUseCase;
            _getEquipmentItemsUseCase = getEquipmentItemsUseCase;
        }
        
        private void MoveItem(int itemId, int toSlotIndex)
        { 
            _moveItemUseCase.Execute(itemId, toSlotIndex);
            FetchItems();
        }

        public void FetchItems()
        {
            var inventoryItems = _getInventoryItemsUseCase.Execute();
            var equipmentItems = _getEquipmentItemsUseCase.Execute();
            State = MapSeparatelyToState(inventoryItems, equipmentItems);
        }
        
        private InventoryState MapSeparatelyToState(Item[] inventoryItems, Item[] equipmentItems)
        {
            return new InventoryState
            {
                InventoryItems = MapToSlotsState(inventoryItems),
                EquipmentItems = MapToSlotsState(equipmentItems)
            };
        }
        
        private InventorySlotState[] MapToSlotsState(Item[] items)
        {
            var state = new InventorySlotState[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                state[i] = new InventorySlotState(
                    slotIndex: i,
                    itemId: items[i]?.Id ?? -1,
                    itemImage: items[i]?.Image,
                    name: items[i]?.Name,
                    onMoveItem: MoveItem
                );
            }

            return state;
        }
    }
}