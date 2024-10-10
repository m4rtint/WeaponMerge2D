using _WeaponMerge.Scripts.Characters.General;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerHealthBehaviour : HealthBehaviour
    {
        protected override void OnHealthChanged()
        {
            Debug.Log("Player health changed to: " + Health);
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
