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
                        return obj;
                    },
                    actionOnGet: (obj) => obj.SetActive(true),
                    actionOnRelease: (obj) => obj.SetActive(false),
                    actionOnDestroy: Destroy,
                    collectionCheck: false,
                    defaultCapacity: 10,
                    maxSize: 50
                );

                _poolDictionary[poolKey] = pool;
            }
        }

        public T Get<T>(Enum poolKey) where T : MonoBehaviour
        {
            if (_poolDictionary.TryGetValue(poolKey, out var value))
            {
                var pooledObject = value.Get();
                return pooledObject.GetComponent<T>();
            }

            throw new Exception("No pool exists for the provided key.");
        }

        public void ReturnToPool(Enum poolKey, GameObject obj)
        {
            if (_poolDictionary.TryGetValue(poolKey, out var value))
            {
                value.Release(obj);
            }
            else
            {
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
        }
    }
}