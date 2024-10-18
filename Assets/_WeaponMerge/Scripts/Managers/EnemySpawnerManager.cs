using System;
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
        [Title("DEBUG")]
        [SerializeField] private bool _isTurnedOn = true;
        
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
                var isSimple = Random.Range(0, 10) % 2 == 0;
                if (isSimple)
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
            Logger.Log("Trying to Spawn Simple Enemy", LogKey.EnemySpawner, gameObject, LogColor.Yellow);
            EnemyBehaviour enemy = ObjectPooler.Instance.Get<EnemyBehaviour>(EnemyType.Simple);
            if (enemy == null)
            {
                Logger.Log("Enemy is null", LogKey.EnemySpawner, gameObject, LogColor.Yellow);
            }
            var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;

            var randomizedPosition = new Vector3(
                position.x + UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x),
                position.y + UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.y),
                0);
            enemy.transform.position = randomizedPosition;
            enemy.Initialize(_playerPositionProvider);
        }

        private void SpawnRangedEnemy()
        {
            Logger.Log("Trying to Spawn Ranged Enemy", LogKey.EnemySpawner, gameObject, LogColor.Yellow);
            RangedEnemyBehaviour enemy = ObjectPooler.Instance.Get<RangedEnemyBehaviour>(EnemyType.Ranged);
            var position = _spawnLocations[Random.Range(0, _spawnLocations.Length)].position;
            enemy.transform.position = new Vector3(
                position.x + UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x),
                position.y + UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.y),
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