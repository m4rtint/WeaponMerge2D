using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyPathFindingBehaviour : MonoBehaviour
    {
        private PlayerPositionProvider _playerPositionProvider = null;
        private SpriteRenderer _spriteRenderer = null;
        [SerializeField]
        private float _speed = 4f; // Speed at which the enemy follows the player
        private bool _canMove = true;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            PanicHelper.CheckAndPanicIfNull(_spriteRenderer);
        }

        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
        }
        
        private void Update()
        {
            //If player is on the right side of the enemy, flip the sprite
            if (_playerPositionProvider == null)
            {
                return;
            }
            _spriteRenderer.flipX = _playerPositionProvider.Get().x > transform.position.x;
            if (!_canMove)
            {
                return;
            }
            
            Vector3 playerPosition = _playerPositionProvider.Get();
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);
        }

        public void Pause()
        {
            _canMove = false;
        }
        
        public void Resume()
        {
            _canMove = true;
        }

        public void CleanUp()
        {
            _canMove = true;
        }
    }
}