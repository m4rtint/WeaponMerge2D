using _WeaponMerge.Scripts.UserInterface;
using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class UserInterfaceCoordinator: MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private Canvas _hudCanvas;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private EquipmentView _equipmentView;
        private bool _isInventoryOpen = false;
        
        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_inventoryCanvas);
            PanicHelper.CheckAndPanicIfNull(_inventoryView);
            PanicHelper.CheckAndPanicIfNull(_equipmentView);
        }

        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnInventoryAction += ToggleInventory;
        }

        public void CleanUp()
        {
            _hudCanvas.gameObject.SetActive(false);
        }

        public void Restart()
        {
            _inventoryCanvas.gameObject.SetActive(false);
            _hudCanvas.gameObject.SetActive(true);
        }

        private void Start()
        {
            var inventoryRepository = new InventoryRepository();
            var moveItemUseCase = new MoveItemUseCase(inventoryRepository);
            var getInventoryItemsUseCase = new GetInventoryItemsUseCase(inventoryRepository);
            var getEquipmentItemsUseCase = new GetEquipmentItemsUseCase(inventoryRepository);
            var inventoryViewModel = new InventoryViewModel(
                moveItemUseCase: moveItemUseCase, 
                getInventoryItemsUseCase: getInventoryItemsUseCase, 
                getEquipmentItemsUseCase: getEquipmentItemsUseCase);
            _inventoryView.Initialize(inventoryViewModel);
            _equipmentView.Initialize(inventoryViewModel);
        }
        
        private void ToggleInventory()
        {
            _isInventoryOpen = !_isInventoryOpen;
            _inventoryCanvas.gameObject.SetActive(_isInventoryOpen);
            _hudCanvas.gameObject.SetActive(_isInventoryOpen);
        }
    }
}