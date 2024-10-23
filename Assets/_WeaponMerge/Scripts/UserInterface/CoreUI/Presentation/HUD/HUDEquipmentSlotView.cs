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

        public void SetState(HUDEquipmentSlotState state)
        {
            _icon.sprite = state.Icon;
            switch (state.Type)
            {
                case HUDEquipmentSlotType.Filled:
                    _selectionIcon.color = Color.clear;
                    _icon.color = Color.white;
                    break;
                case HUDEquipmentSlotType.Empty:
                    _selectionIcon.color = Color.clear;
                    _icon.color = Color.clear;
                    break;
                case HUDEquipmentSlotType.Equipped:
                    _selectionIcon.color = Color.white;
                    _icon.color = Color.white;
                    break;
                default:
                    _selectionIcon.color = Color.clear;
                    _icon.color = Color.clear;
                    break;
            }
        }
    }
}
