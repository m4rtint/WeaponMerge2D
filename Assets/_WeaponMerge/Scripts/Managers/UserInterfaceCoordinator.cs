using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface;
using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.UserInterface.Domain;
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

        [Title("HUD")] 
        [SerializeField] private HUDEquipmentView _hudEquipmentView;
        
        [Title("Views")]
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private EquipmentView _equipmentView;
        
        
        private bool _isInventoryOpen = false;
        private HUDEquipmentViewModel _hudEquipmentViewModel;
        
        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_inventoryCanvas);
            PanicHelper.CheckAndPanicIfNull(_inventoryView);
            PanicHelper.CheckAndPanicIfNull(_equipmentView);
            PanicHelper.CheckAndPanicIfNull(_hudCanvas);
        }

        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnInventoryAction += ToggleInventory;
            controlInput.OnScrollWeaponAction += (_) =>
            {
                _hudEquipmentViewModel.FetchItems();
            };
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
            var getEquippedItemsUseCase = new GetEquippedWeaponUseCase(inventoryRepository);
            var inventoryViewModel = new InventoryViewModel(
                moveItemUseCase: moveItemUseCase, 
                getInventoryItemsUseCase: getInventoryItemsUseCase, 
                getEquipmentItemsUseCase: getEquipmentItemsUseCase);
            _inventoryView.Initialize(inventoryViewModel);
            _equipmentView.Initialize(inventoryViewModel);
            
            _hudEquipmentViewModel = new HUDEquipmentViewModel(getEquipmentItemsUseCase, getEquippedItemsUseCase);
            _hudEquipmentView.Initialize(_hudEquipmentViewModel);
        }
        
        private void ToggleInventory()
        {
            bool isOpeningInventory = !_isInventoryOpen;
            GameStateManager.Instance.ChangeState(isOpeningInventory ? GameState.OpenInventory : GameState.InGame);
            if (GameStateManager.Instance.GetState() == GameState.InGame)
            {
                _hudEquipmentViewModel.FetchItems();
            }

            _isInventoryOpen = isOpeningInventory;
            _inventoryCanvas.gameObject.SetActive(_isInventoryOpen);
            _hudCanvas.gameObject.SetActive(!_isInventoryOpen);
        }
    }
}