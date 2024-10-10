using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _WeaponMerge.Scripts.Managers
{
    public class SandboxWorldManager : MonoBehaviour
    {
        [Title("Configuration")]
        [SerializeField] 
        private InputActionAsset _actionAsset = null;
    
        [Title("Player Components")]
        [SerializeField]
        private PlayerBehaviour _playerBehaviour = null;
        [SerializeField]
        private EnemySpawnerManager _enemySpawnerManager = null;
        private readonly GameStateManager _gameStateManager = GameStateManager.Instance;

        [Button]
        public void SetState(GameState state)
        {
            _gameStateManager.ChangeState(state);
        }
        
        private void Awake()
        {
            _gameStateManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch(state)
            {
                case GameState.InitialState:
                    break;
                case GameState.Loading:
                    Restart();
                    break;
                case GameState.InGame:
                    Resume();
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
            _playerBehaviour.Initialize(controlInput);
            var playerPositionProvider = new PlayerPositionProvider(_playerBehaviour.transform);
            _enemySpawnerManager.Initialize(playerPositionProvider);
            _gameStateManager.ChangeState(GameState.Loading);
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
        
        private void Restart()
        {
            // Populate the world 
            _playerBehaviour.transform.position = Vector3.zero;
            _playerBehaviour.Restart();
            _enemySpawnerManager.Restart();
            _gameStateManager.ChangeState(GameState.InGame);
        }

        private void CleanUp()
        {
            Time.timeScale = 0;
            // Delete and Clean up the world
            _playerBehaviour.CleanUp();
            _enemySpawnerManager.CleanUp();
            ObjectPooler.Instance.CleanUp();
            _gameStateManager.ChangeState(GameState.Loading);
        }
    }
}
