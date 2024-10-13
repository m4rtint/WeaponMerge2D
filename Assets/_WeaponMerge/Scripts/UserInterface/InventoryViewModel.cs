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
        public int Id;
        public Image ItemImage;
        public string Name;
    }
    
    public class InventoryViewModel
    {
        private readonly int _maxInventorySpace;
        private readonly int _maxEquipmentSpace;
        private readonly MoveItemUseCase _moveItemUseCase;
        private readonly GetInventoryItemsUseCase _getInventoryItemsUseCase;
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
            int maxInventorySpace, 
            int maxEquipmentSpace)
        {
            _moveItemUseCase = moveItemUseCase;
            _getInventoryItemsUseCase = getInventoryItemsUseCase;
            _maxInventorySpace = maxInventorySpace;
            _maxEquipmentSpace = maxEquipmentSpace;
        }
        
        public void MoveItem(int itemId, int toSlotIndex)
        {
            var items =  _moveItemUseCase.Execute(itemId, toSlotIndex);
            State = MapToState(items);
        }

        public void FetchItems()
        {
            var items = _getInventoryItemsUseCase.Execute();
            State = MapToState(items);
        }

        private InventoryState MapToState(Item[] items)
        {
            var allItems = new InventorySlotState[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                allItems[i] = new InventorySlotState
                {
                    Id = items[i]?.Id ?? -1,
                    ItemImage = null, //TODO - Set Image when available
                    Name = items[i]?.Name
                };
            }
            
            var inventoryItems = allItems[.._maxInventorySpace];
            var equipmentItems = allItems[_maxInventorySpace..];
            return new InventoryState
            {
                InventoryItems = inventoryItems,
                EquipmentItems = equipmentItems
            };
        }
    }
}