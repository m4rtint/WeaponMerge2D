using System;
using UnityEngine;

namespace _WeaponMerge.Tools
{
    public interface IObjectPooler
    {
        // Creates a pool for a specific enum type and prefab
        void CreatePool<T>(Enum poolKey, GameObject prefab) where T : MonoBehaviour;

        // Gets an object from the pool based on the enum type
        T Get<T>(Enum poolKey) where T : MonoBehaviour;
    }
}