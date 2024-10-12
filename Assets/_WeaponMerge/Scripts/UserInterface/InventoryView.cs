using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class InventoryView : MonoBehaviour
    {
        private InventorySlotBehaviour[] _inventorySlots;

        private void Awake()
        {
            _inventorySlots = GetComponentsInChildren<InventorySlotBehaviour>();
        }
    }
}