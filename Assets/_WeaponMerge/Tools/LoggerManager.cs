using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Tools 
{
    public class LoggerManager : MonoBehaviour
    {
        [InfoBox("List of Logs")]
        [Title("Over World")]
        [SerializeField] private bool _state = false;
        [Title("UserInterface")]
        [SerializeField] private bool _inventory = false;
        [SerializeField] private bool _merge = false;
        [Title("Managers")]
        [SerializeField] private bool _enemySpawner = false;
        [SerializeField] private bool _objectPool = false;
        [Title("Game Mode")]
        [SerializeField] private bool _waveMode = false;
        private void Awake()
        {
            var config = new Dictionary<LogKey, bool>()
            {
                {
                    LogKey.State, _state
                },
                {
                    LogKey.Inventory, _inventory
                },
                {
                    LogKey.Merge, _merge
                },
                {
                    LogKey.EnemySpawner, _enemySpawner
                },
                {
                    LogKey.ObjectPool, _objectPool
                },
                {
                    LogKey.WaveMode, _waveMode
                }
            };
            Logger.Configure(config);
        }
    }
}