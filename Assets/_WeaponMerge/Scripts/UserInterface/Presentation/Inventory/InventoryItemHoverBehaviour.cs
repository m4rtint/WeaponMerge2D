using _WeaponMerge.Scripts.UserInterface.Presentation.Generic;
using _WeaponMerge.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using Logger = _WeaponMerge.Tools.Logger;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Inventory
{
    public class InventoryItemHoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private int _itemId;
        private bool _isHovering;
        private float _hoverTime;
        private const float HoverThreshold = 0.5f; // Time in seconds to consider as hover

        public void OnPointerEnter(PointerEventData eventData)
        {
            Logger.Log("Pointer Enter", LogKey.Inventory, color: LogColor.Green);
            _isHovering = true;
            _hoverTime = 0f;
            _itemId = GetComponent<SlotView>().ItemId;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Logger.Log("Pointer Exit", LogKey.Inventory, color: LogColor.Green);
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
                Logger.Log("Hovering on item: " + _itemId, LogKey.Inventory, color: LogColor.Green);
                ItemDetailView.Instance.ShowItemDetails(_itemId, position: panelPosition);
            }
        }
    }
}