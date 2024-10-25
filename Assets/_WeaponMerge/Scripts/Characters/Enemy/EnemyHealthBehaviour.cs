using _WeaponMerge.Scripts.Characters.General;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyHealthBehaviour : HealthBehaviour
    {
        private EnemyFeedbackEffectBehaviour _enemyFeedbackEffectBehaviour = null;

        private EnemyFeedbackEffectBehaviour VisualBehaviour
        {
            get
            {
                if (_enemyFeedbackEffectBehaviour == null)
                {
                    _enemyFeedbackEffectBehaviour = GetComponent<EnemyFeedbackEffectBehaviour>();
                }

                return _enemyFeedbackEffectBehaviour;
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