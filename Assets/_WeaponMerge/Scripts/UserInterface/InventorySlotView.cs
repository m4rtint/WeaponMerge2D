using System;
using _WeaponMerge.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class InventorySlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        private int _slotIndex;
        private int _itemId;
        private Action<int, int> _onMoveItem;
        
        public void SetState(InventorySlotState state)
        {
            _slotIndex = state.SlotIndex;
            _itemId = state.ItemId;
            _onMoveItem = state.OnMoveItem;
            //Placeholder
            GetComponent<Image>().color = _itemId == -1 ? Color.grey : Color.green;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }
        
        public void OnDrag(PointerEventData eventData)
        {
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var fromSlot = eventData.pointerDrag.GetComponent<InventorySlotView>();
            Tools.Logger.Log("Drag Item from Slot " + fromSlot._slotIndex + " (Item ID: " + fromSlot._itemId + ") to Slot " + _slotIndex, LogKey.Inventory, color: LogColor.Green);
            _onMoveItem?.Invoke(fromSlot._itemId, _slotIndex);
        }
    }
}