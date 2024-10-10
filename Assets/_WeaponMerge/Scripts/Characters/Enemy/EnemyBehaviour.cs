using _WeaponMerge.Scripts.Players;
using UnityEngine;

namespace _WeaponMerge.Scripts.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyPathFindingBehaviour _pathFindingBehaviour = null;

        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
        }
        
        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
        }
    }
}
