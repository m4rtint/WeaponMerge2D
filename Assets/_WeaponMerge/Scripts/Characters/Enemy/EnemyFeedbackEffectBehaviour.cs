using _WeaponMerge.Tools;
using DG.Tweening;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public interface IEnemyFeedbackEffect
    {
        void DamageFlash();
        void DeathAudioEffects(AudioClip[] audioClips, float volume = 1f, bool doRandomPitch = false);
    }
    public class EnemyFeedbackEffectBehaviour: MonoBehaviour, IEnemyFeedbackEffect
    {
        private SpriteRenderer _spriteRenderer = null;
        private AudioSource _audioSource = null;
        private IRandomness _randomness;

        private void Awake()
        {
            _randomness = new Randomness(GetInstanceID());
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void DamageFlash()
        {
            // Flash to white and then back to the original color
            _spriteRenderer.DOColor(Color.red, 0.05f).OnComplete(() =>
            {
                _spriteRenderer.DOColor(Color.white, 0.05f);
            });
        }

        public void DeathAudioEffects(AudioClip[] audioClips, float volume = 1f, bool doRandomPitch = false)
        {
            _audioSource.pitch = doRandomPitch ? _randomness.Range(0.5f, 2f) : 1f;
            _audioSource.volume = volume;
            var clip = GetRandomDeathAudioClip(audioClips);
            _audioSource.PlayOneShot(clip);
        }
        
        private AudioClip GetRandomDeathAudioClip(AudioClip[] audioClips)
        {
            var index = Mathf.Abs(_randomness.Range(0, 10)) % audioClips.Length;
            return audioClips[index];
        }
    }
}