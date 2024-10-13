using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class UserInterfaceCoordinator: MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_inventoryView);
        }

        private void Start()
        {
            var inventoryRepository = new InventoryRepository();
            var moveItemUseCase = new MoveItemUseCase(inventoryRepository);
            var getInventoryItemsUseCase = new GetInventoryItemsUseCase(inventoryRepository);
            var inventoryViewModel = new InventoryViewModel(moveItemUseCase, getInventoryItemsUseCase);
            _inventoryView.Initialize(inventoryViewModel);
        }
    }
}