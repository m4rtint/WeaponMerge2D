using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Logger = _WeaponMerge.Tools.Logger;

namespace _WeaponMerge.Scripts.Managers
{
    public enum EnemyType
    {
        Simple,
        Ranged
    }
    
    public class EnemySpawnerManager: MonoBehaviour
    {
        [Title("DEBUG")]
        [SerializeField] private bool _isTurnedOn = true;
        
        [Header("Spawn Settings")] 
        [SerializeField] private Transform[] _spawnLocations;
        [SerializeField] private Vector2 _spawnArea;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _spawnAmount;
        
        private IRandomness _randomness;
        private float _elapsedSpawnTime = 0f;
        
        private PlayerPositionProvider _playerPositionProvider = null;
        

        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
            _randomness = new Randomness(GetInstanceID().GetHashCode());
        }

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNullOrEmpty(_spawnLocations);
        }

        private void Update()
        {
            if (!_isTurnedOn) return;
            
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
                if (_randomness.CoinFlip())
                {
                    SpawnSimpleEnemy();
                }
                else
                {
                    SpawnRangedEnemy();
                }
            }
        }

        private void SpawnSimpleEnemy()
        {
            EnemyBehaviour enemy = ObjectPooler.Instance.Get<EnemyBehaviour>(EnemyType.Simple);
            var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;

            var randomizedPosition = new Vector3(
                position.x + _randomness.Range(-_spawnArea.x, _spawnArea.x),
                position.y + _randomness.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.transform.position = randomizedPosition;
            enemy.Initialize(_playerPositionProvider);
        }

        private void SpawnRangedEnemy()
        {
            RangedEnemyBehaviour enemy = ObjectPooler.Instance.Get<RangedEnemyBehaviour>(EnemyType.Ranged);
            var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;
            enemy.transform.position = new Vector3(
                position.x + _randomness.Range(-_spawnArea.x, _spawnArea.x),
                position.y + _randomness.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.Initialize(_playerPositionProvider);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 1, 0.5f);
            foreach (var spawnLocation in _spawnLocations)
            {
                Gizmos.DrawCube(spawnLocation.position, new Vector3(_spawnArea.x, _spawnArea.y, 0));
            }
        }
        
        public void Restart()
        {
            _elapsedSpawnTime = 0f;
        }

        public void CleanUp()
        {
            _elapsedSpawnTime = 0f;
        }
    }
}