using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class InventoryView : MonoBehaviour
    {
        private static class Constants
        {
            public const int EmptyItemId = -1;
        }
        
        private InventorySlotView[] _inventorySlots;
        private InventoryViewModel _viewModel;

        private void Awake()
        {
            _inventorySlots = GetComponentsInChildren<InventorySlotView>();
        }
        
        public void Initialize(InventoryViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Start()
        {
            _viewModel.OnStateChanged += OnStateChanged;
            SetupSlots();
            _viewModel.FetchItems();
        }

        private void SetupSlots()
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                _inventorySlots[i].Initialize(
                    slotIndex: i,
                    itemId: Constants.EmptyItemId,
                    onMoveItem: (item, slotIndex) =>
                    {
                        _viewModel.MoveItem(item, slotIndex);
                    });
            }
        }
        
        private void OnStateChanged(InventoryState state)
        {
            for (int i = 0; i < state.InventoryItems.Length; i++)
            {
                _inventorySlots[i].SetItem(state.InventoryItems[i]);
            }
        }
    }
}