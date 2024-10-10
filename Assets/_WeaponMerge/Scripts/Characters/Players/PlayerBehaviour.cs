using _WeaponMerge.Scripts.Managers;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerMovementBehaviour _playerMovementBehaviour = null;
        private PlayerWeaponBehaviour _playerWeaponBehaviour = null;
        private PlayerHealthBehaviour _playerHealthBehaviour = null;
        
        private void Awake()
        {
            _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
            _playerWeaponBehaviour = GetComponent<PlayerWeaponBehaviour>();
            _playerHealthBehaviour = GetComponent<PlayerHealthBehaviour>();
        }
    
        public void Initialize(ControlInput controlInput)
        {
            _playerMovementBehaviour.Initialize(controlInput);
            _playerWeaponBehaviour.Initialize(controlInput);
            _playerHealthBehaviour.Initialize(100, onDeath: () =>
            {
                GameStateManager.Instance.ChangeState(GameState.Restarting);
            });
        }

        public void Restart()
        {
            _playerHealthBehaviour.Restart();
            _playerWeaponBehaviour.Restart();
        }

        public void CleanUp()
        {
            _playerHealthBehaviour.CleanUp();
        }
    }
}
