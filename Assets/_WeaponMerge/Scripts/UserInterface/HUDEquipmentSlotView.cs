using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface
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
            _icon.sprite = state.Icon?.sprite;
            _icon.color = state.Type switch
            {
                HUDEquipmentSlotType.Filled => Color.red,
                HUDEquipmentSlotType.Empty => Color.white,
                HUDEquipmentSlotType.Equipped => Color.green,
                _ => Color.clear
            };
        }
    }
}
