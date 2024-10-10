using _WeaponMerge.Scripts.Characters.General;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyHealthBehaviour: HealthBehaviour
    {
        protected override void OnHealthChanged(int health)
        {
            Debug.Log("Enemy health changed to: " + health);
        }
    }
}