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
            _viewModel.OnStateChanged += OnStateChanged;

        }

        private void Start()
        {
            _viewModel.FetchItems();
        }
        
        private void OnStateChanged(InventoryState state)
        {
            for (var i = 0; i < state.InventoryItems.Length; i++)
            {
                _inventorySlots[i].SetState(state.InventoryItems[i]);
            }
        }
    }
}