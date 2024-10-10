using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _WeaponMerge.Scripts.Managers
{
    public class ControlInput
    {
        private readonly InputActionAsset _actionAsset = null;
    
        // Action Map
        private InputActionMap _playerActionMap = null;
        private InputActionMap _uiActionMap = null;
    
        // Actions
        public event Action<Vector2> OnMoveAction;
    
        public ControlInput(InputActionAsset asset)
        {
            _actionAsset = asset;
            SetupActionMap();
            SetupAction();
        }

        private void SetupActionMap()
        {
            _playerActionMap = _actionAsset.FindActionMap("Player");
            _uiActionMap = _actionAsset.FindActionMap("UI");
        }
    
        private void SetupAction()
        {
            var moveAction = _playerActionMap.FindAction("Move");
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
        }
    
        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();
            OnMoveAction?.Invoke(move);
        }
    }
}
