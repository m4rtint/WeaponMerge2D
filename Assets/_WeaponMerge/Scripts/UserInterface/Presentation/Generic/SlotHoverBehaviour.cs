using _WeaponMerge.Scripts.UserInterface.Presentation.Inventory;
using _WeaponMerge.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using Logger = _WeaponMerge.Tools.Logger;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Generic
{
    public class SlotHoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private int _itemId;
        private bool _isHovering;
        private float _hoverTime;
        private const float HoverThreshold = 0.5f; // Time in seconds to consider as hover

        public void OnPointerEnter(PointerEventData eventData)
        {
            Logger.Log("Pointer Enter: " + name + " | Item ID: " + _itemId , LogKey.Inventory);
            _isHovering = true;
            _hoverTime = 0f;
            _itemId = GetComponent<SlotView>().ItemId;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Logger.Log("Pointer Exit: " + name, LogKey.Inventory);
            _isHovering = false;
            ItemDetailView.Instance.HideItemDetailView();
        }

        private void Update()
        {
            if (_isHovering)
            {
                // increment time even with Timescale is 0
                
                _hoverTime += Time.unscaledDeltaTime;
                if (_hoverTime >= HoverThreshold)
                {
                    OnHover();
                    _isHovering = false;
                }
            }
        }

        private void OnHover()
        {
            if (_itemId != -1)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 panelPosition = new Vector3(mousePos.x, mousePos.y - 50, 0);
                Logger.Log("Hovering on item: " + _itemId, LogKey.Inventory);
                ItemDetailView.Instance.ShowItemDetails(_itemId, position: panelPosition);
            }
        }
    }
}