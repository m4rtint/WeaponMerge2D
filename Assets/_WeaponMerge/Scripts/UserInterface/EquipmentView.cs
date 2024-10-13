using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class EquipmentView : MonoBehaviour
    {
        private InventorySlotView[] _equipmentSlots;
        private InventoryViewModel _viewModel;
        
        private void Awake()
        {
            _equipmentSlots = GetComponentsInChildren<InventorySlotView>();
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
