using _WeaponMerge.Scripts.Managers;
using UnityEngine;

namespace _WeaponMerge.Scripts.Players
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerMovementBehaviour _playerMovementBehaviour = null;
    
        private void Awake()
        {
            _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        }
    
        public void Initialize(ControlInput controlInput)
        {
            _playerMovementBehaviour.Initialize(controlInput);
        }
    }
}
