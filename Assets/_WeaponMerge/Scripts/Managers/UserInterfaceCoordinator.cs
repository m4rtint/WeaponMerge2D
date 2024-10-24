using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Generic;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.HUD;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Merge;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _WeaponMerge.Scripts.Managers
{
    public class UserInterfaceCoordinator: MonoBehaviour
    {
        [Title("Canvas")]
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private Canvas _hudCanvas;
        [SerializeField] private Canvas _mergeCanvas;
        [SerializeField] private Canvas _itemDetailCanvas;

        [Title("HUD")] 
        [SerializeField] private HUDEquipmentView _hudEquipmentView;
        
        [Title("Views")]
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private EquipmentView _equipmentView;
        [SerializeField] private MergeView _mergeView;
        
        [Title("Drag And Drop")]
        [SerializeField] private DragAndDropBehaviour _dragAndDropBehaviour;
        
        private bool _isInventoryOpen = false;
        private bool _isMergeOpen = false;
        private HUDEquipmentViewModel _hudEquipmentViewModel;
        private SyncInventoryUseCase _syncInventoryUseCase;
        
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
            controlInput.OnMergeAction += ToggleMerge;
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
            var moveItemUseCase = new MoveInventoryItemUseCase(inventoryRepository);
            var getInventoryItemsUseCase = new GetInventoryItemsUseCase(inventoryRepository);
            var getEquipmentItemsUseCase = new GetEquipmentItemsUseCase(equipmentRepository);
            var getEquippedItemsUseCase = new GetEquippedWeaponUseCase(equipmentRepository);
            var getEquippedSlotIndexUseCase = new GetEquippedSlotIndexUseCase(equipmentRepository);
            var inventoryViewModel = new InventoryViewModel(
                moveInventoryItemUseCase: moveItemUseCase, 
                getInventoryItemsUseCase: getInventoryItemsUseCase, 
                getEquipmentItemsUseCase: getEquipmentItemsUseCase, 
                dragAndDrop: _dragAndDropBehaviour);
            _inventoryView.Initialize(inventoryViewModel);
            _equipmentView.Initialize(inventoryViewModel);
            
            _hudEquipmentViewModel = new HUDEquipmentViewModel(
                getEquipmentItemsUseCase, 
                getEquippedItemsUseCase, 
                getEquippedSlotIndexUseCase);
            _hudEquipmentView.Initialize(_hudEquipmentViewModel);
            
            var mergeRepository = new MergeRepository(inventoryStorage);
            var moveMergeItemUseCase = new MoveMergeItemUseCase(mergeRepository);
            var getMergeInventoryUseCase = new GetMergeInventoryUseCase(mergeRepository);
            var weaponMergeSystem = new WeaponMergingSystem();
            var mergeItemUseCase = new MergeItemsUseCase(mergeRepository, weaponMergeSystem);
            _syncInventoryUseCase = new SyncInventoryUseCase(mergeRepository);
            _mergeView.Initialize(new MergeViewModel(
                mergeItemsUseCase: mergeItemUseCase,
                moveMergeItemUseCase: moveMergeItemUseCase,
                getMergeInventoryUseCase: getMergeInventoryUseCase));
        }
        
        private void ToggleInventory()
        {
            var currentState = GameStateManager.Instance.GetState();
            if (currentState == GameState.InGame || currentState == GameState.OpenInventory)
            {
                _isInventoryOpen = !_isInventoryOpen;
                GameStateManager.Instance.ChangeState(_isInventoryOpen ? GameState.OpenInventory : GameState.InGame);
                _inventoryCanvas.gameObject.SetActive(_isInventoryOpen);
                _itemDetailCanvas.gameObject.SetActive(_isInventoryOpen);
                _hudCanvas.gameObject.SetActive(!_isInventoryOpen);
            }
        }

        private void ToggleMerge()
        {
            var currentState = GameStateManager.Instance.GetState();
            if (currentState == GameState.InGame || currentState == GameState.OpenMerge)
            {
                bool isOpeningMerge = !_isMergeOpen;
                GameStateManager.Instance.ChangeState(isOpeningMerge ? GameState.OpenMerge : GameState.InGame);
                _isMergeOpen = isOpeningMerge;
                _mergeCanvas.gameObject.SetActive(_isMergeOpen);
                _itemDetailCanvas.gameObject.SetActive(_isMergeOpen);
                _hudCanvas.gameObject.SetActive(!_isMergeOpen);
                if (!_isMergeOpen)
                {
                    _syncInventoryUseCase.Execute();
                }
            }
        }
    }
}