using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface;
using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Scripts.UserInterface.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.Presentation.HUD;
using _WeaponMerge.Scripts.UserInterface.Presentation.Inventory;
using _WeaponMerge.Scripts.UserInterface.Presentation.Merge;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class UserInterfaceCoordinator: MonoBehaviour
    {
        [Title("Canvas")]
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private Canvas _hudCanvas;
        [SerializeField] private Canvas _mergeCanvas;

        [Title("HUD")] 
        [SerializeField] private HUDEquipmentView _hudEquipmentView;
        
        [Title("Views")]
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private EquipmentView _equipmentView;
        [SerializeField] private MergeView _mergeView;
        
        
        private bool _isInventoryOpen = false;
        private HUDEquipmentViewModel _hudEquipmentViewModel;
        
        private void Awake()
        {
            // Canvas
            PanicHelper.CheckAndPanicIfNull(_inventoryCanvas);
            PanicHelper.CheckAndPanicIfNull(_hudCanvas);
            PanicHelper.CheckAndPanicIfNull(_mergeCanvas);
            
            // HUD
            PanicHelper.CheckAndPanicIfNull(_hudEquipmentView);
            
            // Views
            PanicHelper.CheckAndPanicIfNull(_inventoryView);
            PanicHelper.CheckAndPanicIfNull(_equipmentView);
            PanicHelper.CheckAndPanicIfNull(_mergeView);
        }

        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnInventoryAction += ToggleInventory;
        }

        public void CleanUp()
        {
            _inventoryCanvas.gameObject.SetActive(false);
            _hudCanvas.gameObject.SetActive(false);
            _mergeCanvas.gameObject.SetActive(false);
        }

        public void Restart()
        {
            _inventoryCanvas.gameObject.SetActive(false);
            _hudCanvas.gameObject.SetActive(true);
            _mergeCanvas.gameObject.SetActive(false);
        }

        private void Start()
        {
            var inventoryStorage = new InventoryStorage();
            var inventoryRepository = new InventoryRepository(inventoryStorage);
            var equipmentRepository = new EquipmentRepository(inventoryStorage);
            var moveItemUseCase = new MoveItemUseCase(inventoryRepository);
            var getInventoryItemsUseCase = new GetInventoryItemsUseCase(inventoryRepository);
            var getEquipmentItemsUseCase = new GetEquipmentItemsUseCase(equipmentRepository);
            var getEquippedItemsUseCase = new GetEquippedWeaponUseCase(equipmentRepository);
            var inventoryViewModel = new InventoryViewModel(
                moveItemUseCase: moveItemUseCase, 
                getInventoryItemsUseCase: getInventoryItemsUseCase, 
                getEquipmentItemsUseCase: getEquipmentItemsUseCase);
            _inventoryView.Initialize(inventoryViewModel);
            _equipmentView.Initialize(inventoryViewModel);
            
            _hudEquipmentViewModel = new HUDEquipmentViewModel(getEquipmentItemsUseCase, getEquippedItemsUseCase);
            _hudEquipmentView.Initialize(_hudEquipmentViewModel);
            
            var mergeRepository = new MergeRepository(inventoryStorage);
            var setMergeSlotsUseCase = new SetMergeSlotUseCase(mergeRepository);
            _mergeView.Initialize(new MergeViewModel(setMergeSlotsUseCase, getInventoryItemsUseCase));
        }
        
        private void ToggleInventory()
        {
            bool isOpeningInventory = !_isInventoryOpen;
            GameStateManager.Instance.ChangeState(isOpeningInventory ? GameState.OpenInventory : GameState.InGame);
            _isInventoryOpen = isOpeningInventory;
            _inventoryCanvas.gameObject.SetActive(_isInventoryOpen);
            _hudCanvas.gameObject.SetActive(!_isInventoryOpen);
        }
    }
}