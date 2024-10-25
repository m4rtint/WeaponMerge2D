using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Environment
{
    public class DropHealthBehaviour: MonoBehaviour
    {
        [SerializeField]
        private int _health = 30;
        [SerializeField]
        private AudioClip _healthPickUpAudioClip;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFeedbackEffectBehaviour playerFeedbackEffectBehaviour))
            {
                playerFeedbackEffectBehaviour.HealthPickUpAudioEffects(_healthPickUpAudioClip);
            }
            
            if (other.gameObject.TryGetComponent(out PlayerHealthBehaviour player))
            {
                player.GainHealth(_health);
                ObjectPooler.Instance.ReturnToPool(DropType.Health, gameObject);
            }
        }
    }
}