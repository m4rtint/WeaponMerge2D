using _WeaponMerge.Scripts.Environment;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Managers
{
    public class ItemDropManager : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _dropPercentage;
        [SerializeField, Range(0, 1)] private float _dropHealthPercentage;
        [SerializeField, Range(0, 1)] private float _dropWeaponPercentage;

        IRandomness _randomness;

        private void Awake()
        {
            _randomness = new Randomness(GetInstanceID());
        }

        public void DropItemIfNeeded(Vector3 position)
        {
            if (_randomness.Percentage() <= _dropPercentage)
            {
                float totalRatio = _dropHealthPercentage + _dropWeaponPercentage;
                float dropRoll = _randomness.PercentageFloat() * totalRatio;

                if (dropRoll <= _dropHealthPercentage)
                {
                    ObjectPooler.Instance.Get<DropHealthBehaviour>(DropType.Health).transform.position = position;
                }
                else
                {
                    ObjectPooler.Instance.Get<DropItemBehaviour>(DropType.Weapon).transform.position = position;
                }
            }
        }
    }
}