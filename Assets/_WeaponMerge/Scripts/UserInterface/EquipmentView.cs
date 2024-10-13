using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class EquipmentView : MonoBehaviour
    {
        private InventorySlotView[] _equipmentSlots;
        private InventoryViewModel _viewModel;
        private int _firstEquipmentIndex;
        
        private void Awake()
        {
            _equipmentSlots = GetComponentsInChildren<InventorySlotView>();
        }
        
        public void Initialize(InventoryViewModel viewModel, int maxInventorySpace)
        {
            _viewModel = viewModel;
            _viewModel.OnStateChanged += OnStateChanged;
            _firstEquipmentIndex = maxInventorySpace;
            SetupSlots();
            _viewModel.FetchItems();
        }

        private void SetupSlots()
        {
            for(var i = 0; i < _equipmentSlots.Length; i++)
            {
                _equipmentSlots[i].Initialize(
                    slotIndex: _firstEquipmentIndex + i,
                    itemId: -1,
                    onMoveItem: (item, slotIndex) =>
                    {
                        _viewModel.MoveItem(item, slotIndex);
                    });
            }
        }
        
        private void OnStateChanged(InventoryState state)
        {
            for (var i = 0; i < state.EquipmentItems.Length; i++)
            {
                _equipmentSlots[i].SetItem(state.EquipmentItems[i]);
            }
        }
    }
}
