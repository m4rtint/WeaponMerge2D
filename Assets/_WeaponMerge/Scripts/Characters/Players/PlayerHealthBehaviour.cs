using _WeaponMerge.Scripts.Characters.General;
using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerHealthBehaviour : HealthBehaviour
    {
        [SerializeField] private Image _healthBar;
        
        protected override void OnHealthChanged()
        {
            _healthBar.fillAmount = (float)Health / MaxHealth;
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
