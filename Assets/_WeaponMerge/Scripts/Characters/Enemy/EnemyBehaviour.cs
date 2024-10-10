using _WeaponMerge.Scripts.Characters.Players;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyPathFindingBehaviour _pathFindingBehaviour = null;
        private EnemyHealthBehaviour _enemyHealthBehaviour = null;

        private void Awake()
        {
            _pathFindingBehaviour = GetComponent<EnemyPathFindingBehaviour>();
            _enemyHealthBehaviour = GetComponent<EnemyHealthBehaviour>();
        }
        
        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _pathFindingBehaviour.Initialize(playerPositionProvider);
        }
    }
}
