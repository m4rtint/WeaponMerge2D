using System;
using System.Collections.Generic;
using _WeaponMerge.Scripts.Characters.Enemy;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Managers.Domain.UseCases;
using _WeaponMerge.Tools;
using JetBrains.Annotations;
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
        private readonly Queue<EnemyType> _enemyQueue = new Queue<EnemyType>();
        [CanBeNull] private IStoreActiveEnemiesUseCase _storeActiveEnemiesUseCase;
        [CanBeNull] private IncrementEnemiesKilledUseCase _incrementEnemiesKilledUseCase;
        private PlayerPositionProvider _playerPositionProvider = null;
        private ItemDropManager _itemDropManager = null;
        
        [Title("DEBUG")]
        [SerializeField] private bool _isDebugTurnedOn = true;
        
        [Header("Spawn Settings")] 
        [SerializeField] private Transform[] _spawnLocations;
        [SerializeField] private Vector2 _spawnArea;
        [SerializeField] private float _spawnRate = 0.5f;
        
        private IRandomness _randomness;
        private float _elapsedSpawnTime;
        private List<GameObject> _activeEnemies;
        
        public event Action OnClearAllEnemies;

        public void Initialize(
            PlayerPositionProvider playerPositionProvider, 
            ItemDropManager itemDropManager,
            [CanBeNull] IStoreActiveEnemiesUseCase storeActiveEnemiesUseCase = null, 
            [CanBeNull] IncrementEnemiesKilledUseCase incrementEnemiesKilledUseCase = null)
        {
            _playerPositionProvider = playerPositionProvider;
            _itemDropManager = itemDropManager;
            _storeActiveEnemiesUseCase = storeActiveEnemiesUseCase;
            _incrementEnemiesKilledUseCase = incrementEnemiesKilledUseCase;
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
            if (!_isDebugTurnedOn)
            {
                return;
            }
            _storeActiveEnemiesUseCase?.Execute(_enemyQueue.Count + _activeEnemies.Count);
            _elapsedSpawnTime += Time.deltaTime;
            if (_elapsedSpawnTime >= _spawnRate && 
                _enemyQueue.Count > 0)
            {
                _elapsedSpawnTime = 0f;
                Spawn(_enemyQueue.Dequeue());
            }
            else
            {
                CheckIfAllEnemiesAreDead();
            }
        }


        private void CheckIfAllEnemiesAreDead()
        {
            _activeEnemies.RemoveAll(enemy => !enemy.activeInHierarchy);
            Logger.Log($"Active Enemies: {_activeEnemies.Count} | Enemies in Queue: {_enemyQueue.Count}", LogKey.EnemySpawner);
            if (_activeEnemies.Count == 0 && _enemyQueue.Count == 0)
            {
                Logger.Log("All enemies are dead!", LogKey.EnemySpawner);
                // NOTE - Currently it gets called every frame this is true
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

            var position = _spawnLocations[_randomness.Range(0, _spawnLocations.Length)].position;
            var randomizedPosition = new Vector3(
                position.x + _randomness.Range(-_spawnArea.x, _spawnArea.x),
                position.y + _randomness.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.transform.position = randomizedPosition;
            enemy.Initialize(_playerPositionProvider, onDeath: () =>
            {
                _itemDropManager.DropItemIfNeeded(enemy.transform.position);
                _incrementEnemiesKilledUseCase?.Execute();
            });
            _activeEnemies.Add(enemy.gameObject);
        }

        private void SpawnRangedEnemy()
        {
            RangedEnemyBehaviour enemy = ObjectPooler.Instance.Get<RangedEnemyBehaviour>(EnemyType.Ranged);
            Logger.Log($"Spawned Ranged Enemy", LogKey.EnemySpawner, enemy.gameObject);

            var position = _spawnLocations[_randomness.Range(0, _spawnLocations.Length)].position;
            enemy.transform.position = new Vector3(
                position.x + _randomness.Range(-_spawnArea.x, _spawnArea.x),
                position.y + _randomness.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.Initialize(_playerPositionProvider, onDeath: () =>
            {
                _itemDropManager.DropItemIfNeeded(enemy.transform.position);
                _incrementEnemiesKilledUseCase?.Execute();
            });
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