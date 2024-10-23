using DG.Tweening;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Enemy
{
    public class EnemyVisualDamageFlashBehaviour: MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer = null;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void DamageFlash()
        {
            // Flash to white and then back to the original color
            _spriteRenderer.DOColor(Color.red, 0.05f).OnComplete(() =>
            {
                _spriteRenderer.DOColor(Color.white, 0.05f);
            });
        }
    }
}