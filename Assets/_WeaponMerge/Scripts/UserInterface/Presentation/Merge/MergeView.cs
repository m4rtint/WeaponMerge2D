using System;
using _WeaponMerge.Scripts.UserInterface.Presentation.Generic;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Merge
{
    public class MergeView : MonoBehaviour
    {
        [SerializeField] private SlotView _leftSlot;
        [SerializeField] private SlotView _rightSlot;
        [SerializeField] private SlotView[] _inventorySlots;
        
        private MergeViewModel _viewModel;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_leftSlot, nameof(_leftSlot));
            PanicHelper.CheckAndPanicIfNull(_rightSlot, nameof(_rightSlot));
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
        }
        
        private void Start()
        {
            _viewModel.FetchItems();
        }
        
        private void OnStateChanged(MergeViewState state)
        {
            _leftSlot.SetState(state.PrimarySlot);
            _rightSlot.SetState(state.SecondarySlot);
            for (var i = 0; i < state.InventorySlots.Length; i++)
            {
                _inventorySlots[i].SetState(state.InventorySlots[i]);
            }
        }
    }
}
