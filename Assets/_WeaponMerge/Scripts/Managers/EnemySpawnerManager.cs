using System;
using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _WeaponMerge.Scripts.Managers
{
    public enum EnemyType
    {
        Simple    
    }
    
    public class EnemySpawnerManager: MonoBehaviour
    {
        [SerializeField] private EnemyBehaviour _enemyPrefab;

        [Header("Spawn Settings")] 
        [SerializeField] private Transform[] _spawnLocations;
        [SerializeField] private Vector2 _spawnArea;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _spawnAmount;
        
        private float _elapsedSpawnTime = 0f;
        
        private PlayerPositionProvider _playerPositionProvider = null;

        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
        }

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_enemyPrefab);
            PanicHelper.CheckAndPanicIfNullOrEmpty(_spawnLocations);
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
                var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;
                enemy.transform.position = new Vector3(
                    position.x + UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x),
                    position.y + UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.y),
                    0);
                enemy.Initialize(_playerPositionProvider);
            }
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
            ObjectPooler.Instance.CreatePool(EnemyType.Simple, _enemyPrefab);
            _elapsedSpawnTime = 0f;
        }

        public void CleanUp()
        {
            _elapsedSpawnTime = 0f;
        }
    }
}