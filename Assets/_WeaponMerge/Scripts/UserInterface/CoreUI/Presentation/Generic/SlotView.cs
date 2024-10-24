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
        [SerializeField] 
        private Image _itemImage;
        [SerializeField, ReadOnly]
        private int _slotIndex;
        [SerializeField, ReadOnly]
        private int _itemId;

        private SlotState _state;
        public int ItemId => _itemId;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_itemImage);
        }

        public void SetState(SlotState state)
        {
            _slotIndex = state.SlotIndex;
            _itemId = state.ItemId;
            _state = state;
            _itemImage.color = state.ItemImage == null ? Color.clear : Color.white;
            _itemImage.sprite = state.ItemImage;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Tools.Logger.Log("Begin Drag", LogKey.Inventory);
            _state.OnBeginDrag?.Invoke(eventData.position, _itemImage.sprite);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            Tools.Logger.Log("On Drag", LogKey.Inventory);
            _state.OnDragging?.Invoke();
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            Tools.Logger.Log("End Drag", LogKey.Inventory);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var fromSlot = eventData.pointerDrag.GetComponent<SlotView>();
            Tools.Logger.Log("Drop Item from Slot " + fromSlot._slotIndex + " (Item ID: " + fromSlot._itemId + ") to Slot " + _slotIndex, LogKey.Inventory);
            _state.OnMoveItem?.Invoke(fromSlot._itemId, _slotIndex);
        }
    }
}