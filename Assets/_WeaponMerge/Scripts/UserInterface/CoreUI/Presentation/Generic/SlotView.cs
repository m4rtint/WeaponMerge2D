using System;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic
{
    public class SlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField, ReadOnly]
        private int _slotIndex;
        [SerializeField, ReadOnly]
        private int _itemId;
        private Action<int, int> _onMoveItem;
        public int ItemId => _itemId;
        
        public void SetState(SlotState state)
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
            var fromSlot = eventData.pointerDrag.GetComponent<SlotView>();
            Tools.Logger.Log("Drag Item from Slot " + fromSlot._slotIndex + " (Item ID: " + fromSlot._itemId + ") to Slot " + _slotIndex, LogKey.Inventory);
            _onMoveItem?.Invoke(fromSlot._itemId, _slotIndex);
        }
    }
}