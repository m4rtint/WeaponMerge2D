using UnityEngine;

namespace _WeaponMerge.Scripts.Players
{
    public class PlayerPositionProvider
    {
        private readonly Transform _playerTransform = null;
        
        public PlayerPositionProvider(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
        
        public Vector3 Get()
        {
            return _playerTransform.position;
        }
    }
}