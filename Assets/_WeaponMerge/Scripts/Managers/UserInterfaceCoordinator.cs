using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface;
using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class UserInterfaceCoordinator: MonoBehaviour
    {
        private const int MAX_INVENTORY_ITEMS = 16;
        private const int MAX_EQUIPPED_ITEMS = 4;
        
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private EquipmentView _equipmentView;
        
        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_inventoryView);
            PanicHelper.CheckAndPanicIfNull(_equipmentView);
        }

        private void Start()
        {
            var inventoryRepository = new InventoryRepository(MAX_EQUIPPED_ITEMS + MAX_INVENTORY_ITEMS);
            var moveItemUseCase = new MoveItemUseCase(inventoryRepository);
            var getInventoryItemsUseCase = new GetInventoryItemsUseCase(inventoryRepository);
            var inventoryViewModel = new InventoryViewModel(
                moveItemUseCase: moveItemUseCase, 
                getInventoryItemsUseCase: getInventoryItemsUseCase,
                maxInventorySpace: MAX_INVENTORY_ITEMS,
                maxEquipmentSpace: MAX_EQUIPPED_ITEMS);
            _inventoryView.Initialize(inventoryViewModel);
            
            _equipmentView.Initialize(inventoryViewModel, maxInventorySpace: MAX_INVENTORY_ITEMS);
        }
    }
}