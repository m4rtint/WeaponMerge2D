using _WeaponMerge.Tools;
using UnityEngine;
using UnityEngine.Pool;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI
{
    public class DamageNumbersObjectPool : MonoBehaviour
    {
        [SerializeField] private DamageNumberBehaviour _damageNumbersPrefab;
        private ObjectPool<DamageNumberBehaviour> _pool;

        private static DamageNumbersObjectPool _instance;
        public static DamageNumbersObjectPool Instance => _instance;

        private void Awake()
        {
            PanicHelper.CheckAndPanicIfNull(_damageNumbersPrefab);
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            
            // Create the pool
            _pool = new ObjectPool<DamageNumberBehaviour>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
        }
        
        public void ShowDamageNumber(int damageNumber, Vector3 position)
        {
            DamageNumberBehaviour damageText = _pool.Get();
            damageText.SetDamageAndPosition(damageNumber, position);
        }

        public void Release(DamageNumberBehaviour damageNumber)
        {
            _pool.Release(damageNumber);
        }
        
        private DamageNumberBehaviour CreatePooledItem()
        {
            DamageNumberBehaviour instance = Instantiate(_damageNumbersPrefab, this.transform);
            return instance;
        }

        private void OnTakeFromPool(DamageNumberBehaviour item)
        {
            item.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(DamageNumberBehaviour item)
        {
            item.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(DamageNumberBehaviour item)
        {
            Destroy(item.gameObject);
        }
    }
}