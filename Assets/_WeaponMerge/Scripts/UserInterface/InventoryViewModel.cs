using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface
{
    public struct InventoryState
    {
        public InventorySlotState[] InventoryItems;
    }

    public struct InventorySlotState
    {
        public int Id;
        public Image ItemImage;
        public string Name;
    }
    
    public class InventoryViewModel
    {
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
            GetInventoryItemsUseCase getInventoryItemsUseCase)
        {
            _moveItemUseCase = moveItemUseCase;
            _getInventoryItemsUseCase = getInventoryItemsUseCase;
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
            var inventoryItems = new InventorySlotState[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                inventoryItems[i] = new InventorySlotState
                {
                    Id = items[i]?.Id ?? -1,
                    ItemImage = null, //TODO - Set Image when available
                    Name = items[i]?.Name
                };
            }

            return new InventoryState
            {
                InventoryItems = inventoryItems
            };
        }
    }
}