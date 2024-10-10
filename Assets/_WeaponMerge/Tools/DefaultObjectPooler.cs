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

        private readonly Dictionary<Enum, ObjectPool<GameObject>> _poolDictionary = new Dictionary<Enum, ObjectPool<GameObject>>();
        private readonly Dictionary<Enum, Transform> _poolParents = new Dictionary<Enum, Transform>();

        public void CreatePool<T>(Enum poolKey, GameObject prefab) where T : MonoBehaviour
        {
            if (!_poolDictionary.ContainsKey(poolKey))
            {
                GameObject poolParent = new GameObject(poolKey.ToString() + " Pool");
                poolParent.transform.SetParent(transform);
                _poolParents[poolKey] = poolParent.transform;

                ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                    createFunc: () => 
                    {
                        GameObject obj = Instantiate(prefab);
                        obj.transform.SetParent(poolParent.transform);
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
            if (_poolDictionary.ContainsKey(poolKey))
            {
                var pooledObject = _poolDictionary[poolKey].Get();
                return pooledObject.GetComponent<T>();
            }

            throw new Exception("No pool exists for the provided key.");
        }

        public void ReturnToPool(Enum poolKey, GameObject obj)
        {
            if (_poolDictionary.ContainsKey(poolKey))
            {
                _poolDictionary[poolKey].Release(obj);
            }
            else
            {
                Destroy(obj);
            }
        }
    }
}