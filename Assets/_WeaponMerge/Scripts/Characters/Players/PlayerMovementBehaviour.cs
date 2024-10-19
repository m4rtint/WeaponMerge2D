using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 5;
        private Rigidbody2D _rigidbody = null;
        private Vector2 _moveVelocity = Vector2.zero;
        private Animator _animator = null;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            
            PanicHelper.CheckAndPanicIfNull(_animator);
        }
        
        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnMoveAction += OnMove;
        }
    
        private void OnMove(Vector2 move)
        {
            _moveVelocity = move;
            if (move == Vector2.zero)
            {
                _animator.SetInteger("Direction", 0);
            }
            else
            {
                var dir = move.x > 0 ? 1 : (move.x < 0 ? -1 : (move.y != 0 ? 1 : 0));
                _animator.SetInteger("Direction", dir);
            }
            
        }
        
        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _moveVelocity.magnitude > 0 ? _moveVelocity * _moveSpeed : Vector2.zero;
        }
    }
}
