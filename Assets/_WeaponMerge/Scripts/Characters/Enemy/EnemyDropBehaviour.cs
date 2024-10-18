using _WeaponMerge.Scripts.Environment;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyDropBehaviour: MonoBehaviour
    {
        public void Drop()
        {
            var weapon = ObjectPooler.Instance.Get<DropHealthBehaviour>(DropType.Health);
            weapon.transform.position = transform.position;
        }
    }
}