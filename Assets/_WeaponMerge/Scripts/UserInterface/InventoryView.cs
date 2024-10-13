using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface
{
    public class InventoryView : MonoBehaviour
    {
        private InventorySlotView[] _inventorySlots;

        private void Awake()
        {
            _inventorySlots = GetComponentsInChildren<InventorySlotView>();
        }

        private void Start()
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                _inventorySlots[i].Initialize(i);
            }
        }
    }
}