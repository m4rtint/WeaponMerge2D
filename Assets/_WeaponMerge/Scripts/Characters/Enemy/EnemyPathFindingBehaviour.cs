using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using Pathfinding;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyPathFindingBehaviour : MonoBehaviour
    {
        private PlayerPositionProvider _playerPositionProvider = null;
        private SpriteRenderer _spriteRenderer = null;
        private FollowerEntity _followerEntity = null;
        [SerializeField]
        private float _speed = 4f; // Speed at which the enemy follows the player

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _followerEntity = GetComponent<FollowerEntity>();
            PanicHelper.CheckAndPanicIfNull(_spriteRenderer);
            PanicHelper.CheckAndPanicIfNull(_followerEntity);
            _followerEntity.enabled = false;
        }

        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
            _followerEntity.maxSpeed = _speed;
            _followerEntity.enabled = true;
            Resume();
        }

        
        private void Update()
        {
            //If player is on the right side of the enemy, flip the sprite
            if (_playerPositionProvider == null)
            {
                return;
            }
            _spriteRenderer.flipX = _playerPositionProvider.Get().x > transform.position.x;
            
            Vector3 playerPosition = _playerPositionProvider.Get();
            _followerEntity.destination = playerPosition;
            _followerEntity.updateRotation = false;
        }

        public void Pause()
        {
            _followerEntity.isStopped = true;
        }
        
        public void Resume()
        {
            _followerEntity.isStopped = false;
        }

        public void CleanUp()
        {
            _followerEntity.isStopped = true;
            _followerEntity.enabled = false;
        }
    }
}