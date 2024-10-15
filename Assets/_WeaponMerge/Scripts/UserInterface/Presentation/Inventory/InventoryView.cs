using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        private InventorySlotView[] _inventorySlots;
        private InventoryViewModel _viewModel;

        private void Awake()
        {
            _inventorySlots = GetComponentsInChildren<InventorySlotView>();
        }

        private void OnEnable()
        {
            _viewModel?.FetchItems();
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