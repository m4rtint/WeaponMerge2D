using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Tools 
{
    public class LoggerManager : MonoBehaviour
    {
        [InfoBox("List of Logs")]
        [SerializeField] private bool _state = false;
        private void Awake()
        {
            var config = new Dictionary<LogKey, bool>()
            {
                {
                    LogKey.State, _state
                }
            };
            Logger.Configure(config);
        }
    }
}