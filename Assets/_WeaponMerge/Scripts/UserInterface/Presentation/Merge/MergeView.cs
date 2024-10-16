using _WeaponMerge.Scripts.UserInterface.Presentation.Generic;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Merge
{
    public class MergeView : MonoBehaviour
    {
        [Title("Buttons")]
        private Button _mergeButton;
        
        [Title("Slots")]
        [SerializeField] private SlotView _primarySlot;
        [SerializeField] private SlotView _secondarySlot;
        [SerializeField] private SlotView[] _inventorySlots;
        [Space]
        private MergeViewModel _viewModel;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_primarySlot, nameof(_primarySlot));
            PanicHelper.CheckAndPanicIfNull(_secondarySlot, nameof(_secondarySlot));
            PanicHelper.CheckAndPanicIfNullOrEmpty(_inventorySlots, nameof(_inventorySlots));
        }

        private void OnEnable()
        {
            _viewModel?.FetchItems();
        }
        
        public void Initialize(MergeViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnStateChanged += OnStateChanged;
            _viewModel.FetchItems();
            
            _mergeButton.onClick.AddListener(() => _viewModel.MergeItems());
        }
        
        private void OnStateChanged(MergeViewState state)
        {
            _primarySlot.SetState(state.PrimarySlot);
            _secondarySlot.SetState(state.SecondarySlot);
            for (var i = 0; i < state.InventorySlots.Length; i++)
            {
                _inventorySlots[i].SetState(state.InventorySlots[i]);
            }
        }
    }
}
