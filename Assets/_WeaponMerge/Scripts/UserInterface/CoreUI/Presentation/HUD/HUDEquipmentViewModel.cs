using System;
using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.HUD
{
    public enum HUDEquipmentSlotType
    {
        Empty,
        Filled,
        Equipped
    }
    
    public struct HUDEquipmentSlotState
    {
        public HUDEquipmentSlotType Type;
        public Image Icon;
    }
    
    public struct HUDEquipmentState
    {
        public HUDEquipmentSlotState[] Slots;
    }
    
    public class HUDEquipmentViewModel
    {
        private readonly GetEquipmentItemsUseCase _getEquipmentItemsUseCase;
        private readonly GetEquippedWeaponUseCase _getEquippedWeaponUseCase;
        private HUDEquipmentState _state;
        private HUDEquipmentState State
        {
            get => _state;
            set
            {
                _state = value;
                OnStateChanged?.Invoke(value);
            }
        }

        public event Action<HUDEquipmentState> OnStateChanged;
        
        public HUDEquipmentViewModel(
            GetEquipmentItemsUseCase getEquipmentItemsUseCase, 
            GetEquippedWeaponUseCase getEquippedWeaponUseCase)
        {
            _getEquipmentItemsUseCase = getEquipmentItemsUseCase;
            _getEquippedWeaponUseCase = getEquippedWeaponUseCase;
        }
        
        public void FetchItems()
        {
            var equipmentItems = _getEquipmentItemsUseCase.Execute();
            var equippedItem = _getEquippedWeaponUseCase.Execute();
            var newState = MapToSlotStates(equipmentItems, equippedItem);
            if (State.Equals(newState))
            {
                State = newState;
            }
            State = MapToSlotStates(equipmentItems, equippedItem);
        }

        private HUDEquipmentState MapToSlotStates(Item[] items, Item equippedItem)
        {
            HUDEquipmentSlotState[] slotStates = new HUDEquipmentSlotState[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var slotState = new HUDEquipmentSlotState
                {
                    Icon = item?.Image,
                    Type = GetSlotType(item, equippedItem)
                };
                slotStates[i] = slotState;
            }
            var state = new HUDEquipmentState
            {
                Slots = slotStates
            };

            return state;
        }
        
        private HUDEquipmentSlotType GetSlotType(Item item, Item equippedItem)
        {
            if (item == null)
            {
                return HUDEquipmentSlotType.Empty;
            }

            if (equippedItem != null && item.Id == equippedItem.Id)
            {
                return HUDEquipmentSlotType.Equipped;
            }

            return HUDEquipmentSlotType.Filled;
        }
    }
}