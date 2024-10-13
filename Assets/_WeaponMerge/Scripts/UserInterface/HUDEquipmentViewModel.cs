using System;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Domain;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface
{
    public struct HUDEquipmentSlotState
    {
        public bool IsSelected;
        public Image Icon;
    }
    
    public struct HUDEquipmentState
    {
        public HUDEquipmentSlotState[] Slots;
    }
    
    public class HUDEquipmentViewModel
    {
        private readonly GetEquipmentItemsUseCase _getEquipmentItemsUseCase;
        
        private HUDEquipmentState State
        {
            set => OnStateChanged?.Invoke(value);
        }
        
        public event Action<HUDEquipmentState> OnStateChanged;
        
        public HUDEquipmentViewModel(GetEquipmentItemsUseCase getEquipmentItemsUseCase)
        {
            _getEquipmentItemsUseCase = getEquipmentItemsUseCase;
        }
        
        public void FetchItems()
        {
            var equipmentItems = _getEquipmentItemsUseCase.Execute();
            State = MapToSlotStates(equipmentItems);
        }

        private HUDEquipmentState MapToSlotStates(Item[] items)
        {
            HUDEquipmentSlotState[] slotStates = new HUDEquipmentSlotState[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var slotState = new HUDEquipmentSlotState
                {
                    Icon = item?.Image,
                    IsSelected = false
                };
                slotStates[i] = slotState;
            }
            var state = new HUDEquipmentState
            {
                Slots = slotStates
            };

            return state;
        }
    }
}