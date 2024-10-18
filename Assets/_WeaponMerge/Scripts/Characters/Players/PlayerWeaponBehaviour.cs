using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.Managers;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform _weaponTip = null;
        private Weapon _equippedWeapon = null;
        private GetEquippedWeaponUseCase _getEquippedItemUseCase;
        private SwitchEquippedWeaponUseCase _switchEquippedWeaponUseCase;
        private bool _isShootActionPressed = false;
        private float _elapsedCoolDownTime = 0f;

        private void Awake()
        {
            var equipmentRepository = new EquipmentRepository(new InventoryStorage());
            _getEquippedItemUseCase = new GetEquippedWeaponUseCase(equipmentRepository);
            _switchEquippedWeaponUseCase = new SwitchEquippedWeaponUseCase(equipmentRepository);
        }

        public void Initialize(ControlInput controlInput)
        {
            controlInput.OnShootAction += HandleShootAction;
            controlInput.OnScrollWeaponAction += ScrollWeapon;
            GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.InGame)
            {
                var item = _getEquippedItemUseCase.Execute();
                _equippedWeapon = (Weapon)item;
            }
        }

        private void ScrollWeapon(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                var equippedItem = _switchEquippedWeaponUseCase.Execute(direction.y > 0);
                _equippedWeapon = (Weapon)equippedItem;
            }
        }

        private void HandleShootAction(bool onShoot)
        {
            _isShootActionPressed = onShoot;
        }

        private void Update()
        {
            if (_equippedWeapon == null)
            {
                return;
            }

            UpdateCooldownTime();
            HandleShooting();
        }

        private void UpdateCooldownTime()
        {
            if (_elapsedCoolDownTime > 0f)
            {
                _elapsedCoolDownTime -= Time.deltaTime;
            }
        }

        private void HandleShooting()
        {
            if (_isShootActionPressed && _elapsedCoolDownTime <= 0f)
            {
                for (var i = 0; i < _equippedWeapon.BulletsPerShot; i++)
                {
                    Shoot();
                    _elapsedCoolDownTime = _equippedWeapon.FireRate;
                }
            }
        }

        private void Shoot()
        {
            // Get the bullet from the object pool
            var bullet = ObjectPooler.Instance.Get<BulletBehaviour>(_equippedWeapon.AmmoType);

            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Remove the Z axis (since we're in 2D space)
            mousePosition.z = 0f;

            // Calculate the direction from the weapon tip to the mouse position
            Vector2 direction = (mousePosition - _weaponTip.position).normalized;

            // Calculate a random spread angle
            float spreadAngle = Random.Range(-_equippedWeapon.SpreadAngle / 2, _equippedWeapon.SpreadAngle / 2);

            // Rotate the direction by the spread angle
            direction = Quaternion.Euler(0, 0, spreadAngle) * direction;
            
            // Set up the bullet properties
            var bulletProperties = new Bullet(
                _equippedWeapon.BulletSpeed,
                _equippedWeapon.Damage,
                _equippedWeapon.BulletTimeToLive
            );

            // Spawn the bullet at the weapon tip, moving towards the mouse
            bullet.SpawnAt(
                ownerId: gameObject.GetInstanceID(),
                _weaponTip.position,
                bulletProperties,
                direction);
        }

        public void Restart()
        {
            var item = _getEquippedItemUseCase.Execute();
            _equippedWeapon = (Weapon)item;
        }
        
        public void CleanUp()
        {
            _equippedWeapon = null;
        }
    }
}