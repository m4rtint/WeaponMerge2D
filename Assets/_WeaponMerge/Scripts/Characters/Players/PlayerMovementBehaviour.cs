using _WeaponMerge.Scripts.Managers;
using UnityEngine;

namespace _WeaponMerge.Scripts.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 5;
        private Rigidbody2D _rigidbody = null;
        private Vector2 _moveVelocity = Vector2.zero;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnMoveAction += OnMove;
        }
    
        private void OnMove(Vector2 move)
        {
            _moveVelocity = move;
        }
        
        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _moveVelocity.magnitude > 0 ? _moveVelocity * _moveSpeed : Vector2.zero;
        }
    }
}
