using System;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEditor;
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
            _itemImage.transform.localScale = state.ItemImage == null ? Vector3.zero : Vector3.one;
            _itemImage.sprite = state.ItemImage;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Tools.Logger.Log("Begin Drag", LogKey.Inventory);
            HideIcon();
            _state.OnBeginDrag?.Invoke(_itemImage.sprite);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _state.OnDragging?.Invoke();
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            var isEndingOverSlot = eventData.pointerEnter.GetComponent<SlotView>() != null;
            _state.OnEndDrag?.Invoke(this, isEndingOverSlot);
            Tools.Logger.Log("End Drag over GameObject: " + eventData.pointerDrag.name, LogKey.Inventory);
            Tools.Logger.Log("End Drag isOverSlot: " + isEndingOverSlot, LogKey.Inventory);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            var fromSlot = eventData.pointerDrag.GetComponent<SlotView>();
            Tools.Logger.Log("Drop Item from Slot " + fromSlot._slotIndex + " (Item ID: " + fromSlot._itemId + ") to Slot " + _slotIndex, LogKey.Inventory);
            _state.OnMoveItem?.Invoke(fromSlot._itemId, _slotIndex);
        }

        public void ShowIcon()
        {
            _itemImage.transform.localScale = Vector3.one;
        }

        private void HideIcon()
        {
            _itemImage.transform.localScale = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            GUI.color = Color.red;
            Handles.Label(transform.position - Vector3.right * 20, "Slot " + _slotIndex + "\nItem ID: " + _itemId);
        }
    }
}