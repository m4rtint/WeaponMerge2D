using _WeaponMerge.Scripts.Characters.Players;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyPathFindingBehaviour : MonoBehaviour
    {
        private PlayerPositionProvider _playerPositionProvider = null;
        [SerializeField]
        private float _speed = 4f; // Speed at which the enemy follows the player
        
        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
        }
        
        private void Update()
        {
            if (_playerPositionProvider == null)
            {
                return;
            }
            
            Vector3 playerPosition = _playerPositionProvider.Get();
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);
        }
    }
}