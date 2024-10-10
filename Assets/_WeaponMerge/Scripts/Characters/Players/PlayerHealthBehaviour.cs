using _WeaponMerge.Scripts.Characters.General;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerHealthBehaviour : HealthBehaviour
    {
        protected override void OnHealthChanged(int health)
        {
            Debug.Log("Player health changed to: " + health);
        }

        public void Restart()
        {
            Health = MaxHealth;
        }

        public void CleanUp()
        {
            Health = 0;
        }
    }
}
