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
    
        private void Awake()
        {
        
        }

        private void Start()
        {
            ControlInput controlInput = new ControlInput(_actionAsset);
            _playerBehaviour.Initialize(controlInput);
        }
    }
}
