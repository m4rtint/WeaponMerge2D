using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Tools;
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
            _enemyHealthBehaviour.Initialize(10, onDeath: () =>
            {
                ObjectPooler.Instance.ReturnToPool(EnemyType.Simple, gameObject);
            });
        }
    }
}
