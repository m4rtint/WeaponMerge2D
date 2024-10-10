using UnityEngine;

namespace _WeaponMerge.Scripts.Players
{
    public class PlayerWeaponAimBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _weaponPivot = null;
        private SpriteRenderer _weaponSpriteRenderer = null;

        private void Awake()
        {
            _weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
    }
}
