using _WeaponMerge.Scripts.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class InventorySlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        private int _slotIndex;
        private Item _item;

        public void Initialize(int slotIndex)
        {
            _slotIndex = slotIndex;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag item from slot " + _slotIndex);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag item on slot " + _slotIndex);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            // Handle drop logic here
            //Get event Data InventorySLotView FromComponent
            var fromSlot = eventData.pointerDrag.GetComponent<InventorySlotView>();
            Debug.Log("Drag Item from Slot " + fromSlot._slotIndex + " to Slot " + _slotIndex);
        }
    }
}