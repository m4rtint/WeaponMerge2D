using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.HUD
{
    public class HUDEquipmentView : MonoBehaviour
    {
        private HUDEquipmentSlotView[] _equipmentSlots;
        private HUDEquipmentViewModel _viewModel;
        
        private void Awake()
        {
            _equipmentSlots = GetComponentsInChildren<HUDEquipmentSlotView>();
        }

        public void Initialize(HUDEquipmentViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnStateChanged += OnStateChanged;
        }

        private void Update()
        {
            _viewModel?.FetchItems();
        }

        private void OnStateChanged(HUDEquipmentState state)
        {
            for (var i = 0; i < state.Slots.Length; i++)
            {
                _equipmentSlots[i].SetState(state.Slots[i] ,i == state.EquippedSlotIndex);
            }
        }
    }
}
