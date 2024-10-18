using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic;
using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory
{
    public class EquipmentView : MonoBehaviour
    {
        private SlotView[] _equipmentSlots;
        private InventoryViewModel _viewModel;
        
        private void Awake()
        {
            _equipmentSlots = GetComponentsInChildren<SlotView>();
        }
        
        public void Initialize(InventoryViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnStateChanged += OnStateChanged;
            _viewModel.FetchItems();
        }
        
        private void OnStateChanged(InventoryState state)
        {
            for (var i = 0; i < state.EquipmentItems.Length; i++)
            {
                _equipmentSlots[i].SetState(state.EquipmentItems[i]);
            }
        }
    }
}
