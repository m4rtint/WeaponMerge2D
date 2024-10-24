using _WeaponMerge.Tools;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public interface IPlayerFeedbackEffects
    {
        void ShootVisualEffects();
        void ShootAudioEffects(AudioClip clip);
    }
    
    public class PlayerFeedbackEffectBehaviour : MonoBehaviour, IPlayerFeedbackEffects
    {
        [Title("Weapon Visuals")]
        [SerializeField] private SpriteRenderer _muzzleFlashSpriteRenderer = null;
        [SerializeField] private Transform _weaponPivot = null;
        
        [Title("Weapon Audio")]
        [SerializeField] private AudioSource _audioSource = null;
        
        private SpriteRenderer _weaponSpriteRenderer = null;
         

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            PanicHelper.CheckAndPanicIfNull(_weaponSpriteRenderer);
            PanicHelper.CheckAndPanicIfNull(_weaponPivot);
            PanicHelper.CheckAndPanicIfNull(_audioSource);
        }

        private void Start()
        {
            _muzzleFlashSpriteRenderer.color = Color.clear;
        }

        private void Update()
        {
            // Get the mouse position in world space and ensure it's 2D
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // Calculate the direction and angle to the mouse position
            Vector2 aimDirection = (mousePosition - _weaponPivot.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            // Rotate the weapon around the pivot point
            _weaponPivot.rotation = Quaternion.Euler(0, 0, angle);

            // Flip the weapon sprite when aiming to the left
            _weaponSpriteRenderer.flipY = aimDirection.x < 0;
        }

        public void ShootVisualEffects()
        {
            // Make the color appear for 1 second using DOColor
            _muzzleFlashSpriteRenderer.DOColor(Color.white, 0.1f) // Flash the color to white
                .OnComplete(() =>
                {
                    // Return to the original color (assuming it's transparent)
                    _muzzleFlashSpriteRenderer.DOColor(Color.clear, 0.1f);
                });
        }

        public void ShootAudioEffects(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
