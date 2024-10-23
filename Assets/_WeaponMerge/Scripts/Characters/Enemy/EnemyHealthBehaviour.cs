using _WeaponMerge.Scripts.Characters.General;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyHealthBehaviour : HealthBehaviour
    {
        private EnemyVisualDamageFlashBehaviour _enemyVisualDamageFlashBehaviour = null;

        private EnemyVisualDamageFlashBehaviour VisualBehaviour
        {
            get
            {
                if (_enemyVisualDamageFlashBehaviour == null)
                {
                    _enemyVisualDamageFlashBehaviour = GetComponent<EnemyVisualDamageFlashBehaviour>();
                }

                return _enemyVisualDamageFlashBehaviour;
            }
        }
        
        protected override void OnHealthChanged()
        {
            if (VisualBehaviour == null)
            {
                Debug.LogWarning("EnemyVisualDamageFlashBehaviour component is missing on this enemy.");            
            }
            VisualBehaviour?.DamageFlash();
        }
    }
}