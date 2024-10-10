using _WeaponMerge.Scripts.Managers;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerMovementBehaviour _playerMovementBehaviour = null;
        private PlayerWeaponBehaviour _playerWeaponBehaviour = null;
        
        private void Awake()
        {
            _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
            _playerWeaponBehaviour = GetComponent<PlayerWeaponBehaviour>();
        }
    
        public void Initialize(ControlInput controlInput)
        {
            _playerMovementBehaviour.Initialize(controlInput);
            _playerWeaponBehaviour.Initialize(controlInput);
        }
    }
}
