using _WeaponMerge.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.HUD
{
    [RequireComponent(typeof(Image))]
    public class HUDEquipmentSlotView : MonoBehaviour
    {
        [SerializeField] private Image _selectionIcon;
        [SerializeField] private Image _icon;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_selectionIcon);
            PanicHelper.CheckAndPanicIfNull(_icon);
        }

        public void SetState(HUDEquipmentSlotState state, bool isSelected)
        {
            _selectionIcon.color = isSelected ? Color.white : Color.clear;
            _icon.sprite = state.Icon;
            switch (state.Type)
            {
                case HUDEquipmentSlotType.Filled:
                    _icon.color = Color.white;
                    break;
                case HUDEquipmentSlotType.Empty:
                    _icon.color = Color.clear;
                    break;
                case HUDEquipmentSlotType.Equipped:
                    _icon.color = Color.white;
                    break;
                default:
                    _icon.color = Color.clear;
                    break;
            }
        }
    }
}
