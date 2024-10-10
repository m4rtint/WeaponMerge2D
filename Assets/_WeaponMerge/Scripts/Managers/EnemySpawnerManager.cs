using _WeaponMerge.Scripts.Enemy;
using _WeaponMerge.Scripts.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public enum EnemyType
    {
        Simple    
    }
    
    public class EnemySpawnerManager: MonoBehaviour
    {
        [SerializeField]
        private EnemyBehaviour _enemyPrefab;
        [SerializeField]
        private Vector2 _spawnArea;
        [SerializeField]
        private float _spawnRate;
        [SerializeField] 
        private int _spawnAmount;
        
        private float _elapsedSpawnTime = 0f;
        
        private PlayerPositionProvider _playerPositionProvider = null;

        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
        }
        
        private void Start()
        {
            ObjectPooler.Instance.CreatePool(EnemyType.Simple, _enemyPrefab);
        }

        private void Update()
        {
            _elapsedSpawnTime += Time.deltaTime;
            if (_elapsedSpawnTime >= _spawnRate)
            {
                _elapsedSpawnTime = 0f;
                Spawn();
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                var enemy = ObjectPooler.Instance.Get<EnemyBehaviour>(EnemyType.Simple);
                enemy.transform.position = new Vector3(
                    transform.position.x + UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x),
                    transform.position.y + UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.y),
                    0);
                enemy.Initialize(_playerPositionProvider);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1, 1, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(_spawnArea.x, _spawnArea.y, 0));
        }
    }
}