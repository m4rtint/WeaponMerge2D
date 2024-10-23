using System;
using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using UnityEngine;

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
        public Sprite Icon;
    }
    
    public struct HUDEquipmentState
    {
        public HUDEquipmentSlotState[] Slots;
        public int EquippedSlotIndex;
    }
    
    public class HUDEquipmentViewModel
    {
        private readonly GetEquipmentItemsUseCase _getEquipmentItemsUseCase;
        private readonly GetEquippedWeaponUseCase _getEquippedWeaponUseCase;
        private readonly GetEquippedSlotIndexUseCase _getEquippedSlotIndex;
        private HUDEquipmentState State
        {
            set => OnStateChanged?.Invoke(value);
        }

        public event Action<HUDEquipmentState> OnStateChanged;
        
        public HUDEquipmentViewModel(
            GetEquipmentItemsUseCase getEquipmentItemsUseCase, 
            GetEquippedWeaponUseCase getEquippedWeaponUseCase,
            GetEquippedSlotIndexUseCase getEquippedSlotIndex)
        {
            _getEquipmentItemsUseCase = getEquipmentItemsUseCase;
            _getEquippedWeaponUseCase = getEquippedWeaponUseCase;
            _getEquippedSlotIndex = getEquippedSlotIndex;
        }
        
        public void FetchItems()
        {
            var index = _getEquippedSlotIndex.Execute();
            var equipmentItems = _getEquipmentItemsUseCase.Execute();
            var equippedItem = _getEquippedWeaponUseCase.Execute();
            State = MapToSlotStates(equipmentItems, equippedItem, index);
        }

        private HUDEquipmentState MapToSlotStates(Item[] items, Item equippedItem, int equippedSlotIndex)
        {
            HUDEquipmentSlotState[] slotStates = new HUDEquipmentSlotState[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var slotState = new HUDEquipmentSlotState
                {
                    Icon = item?.Sprite,
                    Type = GetSlotType(item, equippedItem)
                };
                slotStates[i] = slotState;
            }
            var state = new HUDEquipmentState
            {
                Slots = slotStates,
                EquippedSlotIndex = equippedSlotIndex
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