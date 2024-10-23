using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerMovementBehaviour _playerMovementBehaviour = null;
        private PlayerWeaponBehaviour _playerWeaponBehaviour = null;
        private PlayerHealthBehaviour _playerHealthBehaviour = null;
        private PlayerWeaponSpriteBehaviour _playerWeaponSpriteBehaviour = null;
        
        private void Awake()
        {
            _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
            _playerWeaponBehaviour = GetComponent<PlayerWeaponBehaviour>();
            _playerHealthBehaviour = GetComponent<PlayerHealthBehaviour>();
            _playerWeaponSpriteBehaviour = GetComponentInChildren<PlayerWeaponSpriteBehaviour>();
            
            PanicHelper.CheckAndPanicIfNull(_playerMovementBehaviour);
            PanicHelper.CheckAndPanicIfNull(_playerWeaponBehaviour);
            PanicHelper.CheckAndPanicIfNull(_playerHealthBehaviour);
            PanicHelper.CheckAndPanicIfNull(_playerWeaponSpriteBehaviour);
        }
    
        public void Initialize(ControlInput controlInput)
        {
            _playerMovementBehaviour.Initialize(controlInput);
            _playerWeaponBehaviour.Initialize(controlInput, _playerWeaponSpriteBehaviour);
            _playerHealthBehaviour.Initialize(100);
            
            _playerHealthBehaviour.SetDeathActions(0, 
                onDeath: () =>
                {
                    
                }, 
                onCleanUp: () =>
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
            _playerWeaponBehaviour.CleanUp();
        }
    }
}
