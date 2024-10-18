using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _WeaponMerge.Tools
{
    public class ObjectPooler : MonoBehaviour, IObjectPooler
    {
        public static ObjectPooler Instance => _instance;
        private static ObjectPooler _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private readonly Dictionary<Enum, ObjectPool<GameObject>> _poolDictionary = new();
        private readonly List<GameObject> _allCreatedObjects = new();

        public void CreatePool<T>(Enum poolKey, T prefab) where T : MonoBehaviour
        {
            if (!_poolDictionary.ContainsKey(poolKey))
            {
                var poolName = typeof(T).Name + " Pool";
                GameObject poolParent = new GameObject(poolName);
                poolParent.transform.SetParent(transform);

                ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                    createFunc: () =>
                    {
                        GameObject obj = Instantiate(prefab.gameObject);
                        obj.transform.SetParent(poolParent.transform);
                        _allCreatedObjects.Add(obj);
                        Logger.Log($"Created new object for pool {poolKey.GetType().Name} {poolKey}", LogKey.ObjectPool);
                        return obj;
                    },
                    actionOnGet: (obj) => obj.SetActive(true),
                    actionOnRelease: (obj) => obj.SetActive(false),
                    actionOnDestroy: Destroy,
                    collectionCheck: true,
                    defaultCapacity: 50,
                    maxSize: 200
                );

                _poolDictionary[poolKey] = pool;
            }
        }

        public T Get<T>(Enum poolKey) where T : MonoBehaviour
        {
            if (_poolDictionary.TryGetValue(poolKey, out var value))
            {
                var pooledObject = value.Get();
                var component = pooledObject.GetComponent<T>();
                if (component == null)
                {
                    Logger.Log($"Component is null. PoolKey: {poolKey.GetType().Name} {poolKey}, GameObject: {pooledObject.name}", LogKey.ObjectPool, pooledObject);
                    throw new Exception("Component is null. Check Create and Release methods");
                }

                return component;
            }

            throw new Exception("No pool exists for the provided key.");
        }

        public void ReturnToPool(Enum poolKey, GameObject obj)
        {
            if (_poolDictionary.TryGetValue(poolKey, out var value))
            {
                Logger.Log($"Returned object to pool {poolKey.GetType().Name} {poolKey}, GameObject: {obj.name}", LogKey.ObjectPool, obj);
                value.Release(obj);
            }
            else
            {
                Logger.Log($"No pool exists for the provided key. PoolKey: {poolKey.GetType().Name} {poolKey}, GameObject: {obj.name}", LogKey.ObjectPool, obj);
                Destroy(obj);
            }
        }

        public void CleanUp()
        {
            foreach (var obj in _allCreatedObjects)
            {
                Destroy(obj);
            }
            _allCreatedObjects.Clear();
            _poolDictionary.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}