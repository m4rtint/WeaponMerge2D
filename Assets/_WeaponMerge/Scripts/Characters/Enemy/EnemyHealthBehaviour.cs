using _WeaponMerge.Scripts.Characters.General;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyHealthBehaviour: HealthBehaviour
    {
        protected override void OnHealthChanged()
        {
            if (Health < MaxHealth)
            {
                // Do something
            }
            // Nothing here
        }
    }
}