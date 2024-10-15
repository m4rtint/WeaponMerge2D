using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Scripts.UserInterface.Domain.UseCases;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Inventory
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
        public Image ItemImage { get; }
        public string Name { get; }
        public Action<int, int> OnMoveItem { get; }
        
        public SlotState(int slotIndex, int itemId, Image itemImage, string name, Action<int, int> onMoveItem)
        {
            SlotIndex = slotIndex;
            ItemId = itemId;
            ItemImage = itemImage;
            Name = name;
            OnMoveItem = onMoveItem;
        }
    }
    
    public class InventoryViewModel
    {
        private readonly MoveInventoryItemUseCase _moveInventoryItemUseCase;
        private readonly GetInventoryItemsUseCase _getInventoryItemsUseCase;
        private readonly GetEquipmentItemsUseCase _getEquipmentItemsUseCase;

        private InventoryState State
        {
            set => OnStateChanged?.Invoke(value);
        }
        
        public event Action<InventoryState> OnStateChanged;
        
        public InventoryViewModel(
            MoveInventoryItemUseCase moveInventoryItemUseCase, 
            GetInventoryItemsUseCase getInventoryItemsUseCase,
            GetEquipmentItemsUseCase getEquipmentItemsUseCase)
        {
            _moveInventoryItemUseCase = moveInventoryItemUseCase;
            _getInventoryItemsUseCase = getInventoryItemsUseCase;
            _getEquipmentItemsUseCase = getEquipmentItemsUseCase;
        }
        
        private void MoveItem(int itemId, int toSlotIndex)
        { 
            _moveInventoryItemUseCase.Execute(itemId, toSlotIndex);
            FetchItems();
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
                    itemImage: items[i]?.Image,
                    name: items[i]?.Name,
                    onMoveItem: MoveItem
                );
            }

            return state;
        }
    }
}