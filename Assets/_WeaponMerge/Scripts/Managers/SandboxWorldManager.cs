using _WeaponMerge.Scripts.Enemy;
using _WeaponMerge.Scripts.Players;
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
        
        [Title("Enemy Components")]
        [SerializeField]
        private EnemyBehaviour _enemyBehaviour = null;
        
        private void Awake()
        {
        
        }

        private void Start()
        {
            var controlInput = new ControlInput(_actionAsset);
            _playerBehaviour.Initialize(controlInput);
            var playerPositionProvider = new PlayerPositionProvider(_playerBehaviour.transform);
            _enemyBehaviour.Initialize(playerPositionProvider);
        }
    }
}
