using _WeaponMerge.Scripts.Characters.Players;
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
        
        private void Awake()
        {
        
        }

        private void Start()
        {
            var controlInput = new ControlInput(_actionAsset);
            _playerBehaviour.Initialize(controlInput);
            var playerPositionProvider = new PlayerPositionProvider(_playerBehaviour.transform);
            _enemySpawnerManager.Initialize(playerPositionProvider);
        }
    }
}
