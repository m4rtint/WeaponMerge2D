using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Managers.Data;
using _WeaponMerge.Scripts.Managers.Domain.UseCases;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _WeaponMerge.Scripts.Managers
{
    public class WaveModeWorldManager : MonoBehaviour
    {
        private readonly GameStateManager _gameStateManager = GameStateManager.Instance;

        [Title("Configuration")] 
        [SerializeField] private InputActionAsset _actionAsset = null;
        [SerializeField] private PrefabPoolCoordinator _prefabPoolCoordinator = null;

        [Title("Components")] 
        [SerializeField] private PlayerBehaviour _playerBehaviour = null;
        [SerializeField] private EnemySpawnerManager _enemySpawnerManager = null;
        [SerializeField] private ItemDropManager _itemDropManager = null;

        [Title("User Interface / HUD")] [SerializeField]
        private UserInterfaceCoordinator _userInterfaceCoordinator = null;

        private WaveModeSystem _waveModeSystem;
        private WaveModeRepository _waveRepository;
        
        [Button]
        public void SetState(GameState state)
        {
            _gameStateManager.ChangeState(state);
        }

        private void Awake()
        {
            _gameStateManager.OnGameStateChanged += OnGameStateChanged;
            PanicHelper.CheckAndPanicIfNull(_actionAsset);
            PanicHelper.CheckAndPanicIfNull(_playerBehaviour);
            PanicHelper.CheckAndPanicIfNull(_enemySpawnerManager);
            PanicHelper.CheckAndPanicIfNull(_itemDropManager);
            PanicHelper.CheckAndPanicIfNull(_prefabPoolCoordinator);
            PanicHelper.CheckAndPanicIfNull(_userInterfaceCoordinator);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.InitialState:
                    break;
                case GameState.Loading:
                    Restart();
                    break;
                case GameState.InGame:
                    Resume();
                    break;
                case GameState.OpenInventory:
                    OpenInventory();
                    break;
                case GameState.OpenMerge:
                    OpenMerge();
                    break;
                case GameState.Paused:
                    Pause();
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Restarting:
                    CleanUp();
                    break;
            }
        }

        private void Start()
        {   
            var controlInput = new ControlInput(_actionAsset);
            var playerPositionProvider = new PlayerPositionProvider(_playerBehaviour.transform);
            _waveRepository = new WaveModeRepository();
            var storeWaveRoundNumberUseCase = new StoreWaveRoundNumberUseCase(_waveRepository);
            _waveModeSystem = new WaveModeSystem(
                _enemySpawnerManager, 
                storeWaveRoundNumberUseCase);
            _playerBehaviour.Initialize(controlInput);
            _enemySpawnerManager.Initialize(
                playerPositionProvider,
                _itemDropManager,
                storeActiveEnemiesUseCase: new StoreWaveActiveEnemiesUseCase(_waveRepository), 
                incrementEnemiesKilledUseCase: new IncrementEnemiesKilledUseCase(_waveRepository));
            _userInterfaceCoordinator.Initialize(controlInput);
            _gameStateManager.ChangeState(GameState.Loading);
        }

        private void AddStartingPistol()
        {
            var inventory = new InventoryRepository(new InventoryStorage());
            inventory.AddItem(new WeaponsFactory().CreateWeapon(WeaponType.Pistol));
        }

        private void Pause()
        {
            // Pause the game
            Time.timeScale = 0;
        }

        private void Resume()
        {
            Time.timeScale = 1;
        }

        private void OpenInventory()
        {
            Time.timeScale = 0;
        }

        private void OpenMerge()
        {
            Time.timeScale = 0;
        }

        private void Restart()
        {
            AddStartingPistol();

            // Populate the world 
            _playerBehaviour.transform.position = Vector3.zero;
            _playerBehaviour.Restart();
            _enemySpawnerManager.Restart();
            _prefabPoolCoordinator.Restart();
            _userInterfaceCoordinator.Restart();
            _gameStateManager.ChangeState(GameState.InGame);
            // Start Waves
            _waveModeSystem.Start();
        }

        private void CleanUp()
        {
            Time.timeScale = 0;
            // Delete and Clean up the world
            _playerBehaviour.CleanUp();
            _enemySpawnerManager.CleanUp();
            ObjectPooler.Instance.CleanUp();
            _userInterfaceCoordinator.CleanUp();
            _waveRepository.CleanUp();
            _waveModeSystem.CleanUp();
            _gameStateManager.ChangeState(GameState.Loading);
        }
    }
}