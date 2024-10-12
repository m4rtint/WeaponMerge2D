using _WeaponMerge.Scripts.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class InventorySlotBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Item _item;
        private bool _isHovering;
        private float _hoverTime;
        private const float HoverThreshold = 1.0f; // Time in seconds to consider as hover

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovering = true;
            _hoverTime = 0f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovering = false;
        }

        private void Update()
        {
            if (_isHovering)
            {
                _hoverTime += Time.deltaTime;
                if (_hoverTime >= HoverThreshold)
                {
                    OnHover();
                    _isHovering = false;
                }
            }
        }

        private void OnHover()
        {
            // Handle hover logic here
            Debug.Log("Hovered over item slot for 1 second.");
        }
    }
}