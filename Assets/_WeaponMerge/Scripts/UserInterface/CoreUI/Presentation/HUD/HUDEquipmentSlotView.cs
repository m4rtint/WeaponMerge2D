using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.HUD
{
    [RequireComponent(typeof(Image))]
    public class HUDEquipmentSlotView : MonoBehaviour
    {
        private Image _icon;

        private void Awake()
        {
            _icon = GetComponent<Image>();
        }

        public void SetState(HUDEquipmentSlotState state)
        {
            _icon.sprite = state.Icon;
            _icon.color = state.Type switch
            {
                HUDEquipmentSlotType.Filled => Color.white,
                HUDEquipmentSlotType.Empty => Color.black,
                HUDEquipmentSlotType.Equipped => Color.white,
                _ => Color.clear
            };
        }
    }
}
