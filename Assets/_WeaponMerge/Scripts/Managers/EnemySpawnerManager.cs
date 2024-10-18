using System;
using System.Collections.Generic;
using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using Logger = _WeaponMerge.Tools.Logger;
using Random = UnityEngine.Random;

namespace _WeaponMerge.Scripts.Managers
{
    public enum EnemyType
    {
        Simple,
        Ranged
    }
    
    public class EnemySpawnerManager: MonoBehaviour
    {
        private readonly Queue<EnemyType> _enemyQueue = new Queue<EnemyType>();
        private PlayerPositionProvider _playerPositionProvider = null;

        [Title("DEBUG")]
        [SerializeField] private bool _isTurnedOn = true;
        
        [Header("Spawn Settings")] 
        [SerializeField] private Transform[] _spawnLocations;
        [SerializeField] private Vector2 _spawnArea;
        [SerializeField] private float _spawnRate = 0.5f;
        
        private IRandomness _randomness;
        private float _elapsedSpawnTime;
        private List<GameObject> _activeEnemies;
        
        public event Action OnClearAllEnemies;

        public void Initialize(PlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
            _randomness = new Randomness(GetInstanceID().GetHashCode());
        }
        
        public void SetEnemiesToSpawn(EnemyType[] enemies)
        {
            _enemyQueue.Clear();
            foreach (var enemy in enemies)
            {
                _enemyQueue.Enqueue(enemy);
            }
        }

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNullOrEmpty(_spawnLocations);
        }

        private void Update()
        {
            if (!_isTurnedOn && _enemyQueue.Count == 0) return;
            
            _elapsedSpawnTime += Time.deltaTime;
            if (_elapsedSpawnTime >= _spawnRate)
            {
                _elapsedSpawnTime = 0f;
                Spawn(_enemyQueue.Dequeue());
            }

            CheckIfAllEnemiesAreDead();
        }
        
        private void CheckIfAllEnemiesAreDead()
        {
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                if (!_activeEnemies[i].activeInHierarchy)
                {
                    _activeEnemies.RemoveAt(i);
                }
            }
            
            if (_activeEnemies.Count == 0 && _enemyQueue.Count == 0)
            {
                OnClearAllEnemies?.Invoke();
            }
        }

        private void Spawn(EnemyType spawnTypes)
        {
            switch (spawnTypes)
            {
                case EnemyType.Simple:
                    SpawnSimpleEnemy();
                    break;
                case EnemyType.Ranged:
                    SpawnRangedEnemy();
                    break;
            }
        }

        private void SpawnSimpleEnemy()
        {
            EnemyBehaviour enemy = ObjectPooler.Instance.Get<EnemyBehaviour>(EnemyType.Simple);
            Logger.Log($"Spawned Simple Enemy", LogKey.EnemySpawner, enemy.gameObject);

            var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;
            var randomizedPosition = new Vector3(
                position.x + _randomness.Range(-_spawnArea.x, _spawnArea.x),
                position.y + _randomness.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.transform.position = randomizedPosition;
            enemy.Initialize(_playerPositionProvider);
            _activeEnemies.Add(enemy.gameObject);
        }

        private void SpawnRangedEnemy()
        {
            RangedEnemyBehaviour enemy = ObjectPooler.Instance.Get<RangedEnemyBehaviour>(EnemyType.Ranged);
            Logger.Log($"Spawned Ranged Enemy", LogKey.EnemySpawner, enemy.gameObject);

            var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;
            enemy.transform.position = new Vector3(
                position.x + _randomness.Range(-_spawnArea.x, _spawnArea.x),
                position.y + _randomness.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.Initialize(_playerPositionProvider);
            _activeEnemies.Add(enemy.gameObject);
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
            _activeEnemies = new List<GameObject>();
        }

        public void CleanUp()
        {
            _elapsedSpawnTime = 0f;
            _activeEnemies.Clear();
            _activeEnemies = null;
        }
    }
}