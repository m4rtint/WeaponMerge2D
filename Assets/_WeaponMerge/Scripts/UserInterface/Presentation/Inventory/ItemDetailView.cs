using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.UserInterface.Domain;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using TMPro;
using UnityEngine;

namespace _WeaponMerge.Scripts.UserInterface.Presentation.Inventory
{
    public class ItemDetailView : MonoBehaviour
    {
        private GetInventoryItemUseCase _getInventoryItemUseCase;
        
        private static ItemDetailView _instance;

        public static ItemDetailView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<ItemDetailView>();
                    if (_instance == null)
                    {
                        PanicHelper.CheckAndPanicIfNull(_instance);
                    }
                }
                return _instance;
            }
        }

        private TMP_Text _itemDetailText = null;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;

            _getInventoryItemUseCase = new GetInventoryItemUseCase(new InventoryRepository(new InventoryStorage()));
            _itemDetailText = GetComponentInChildren<TMP_Text>();
            PanicHelper.CheckAndPanicIfNull(_itemDetailText, nameof(_itemDetailText));
            gameObject.SetActive(false);
        }

        public void ShowItemDetails(int itemId, Vector3 position)
        {
            if (itemId == -1)
            {
                _itemDetailText.text = string.Empty;
                gameObject.SetActive(false);
                return;
            }
            var item = _getInventoryItemUseCase.Execute(itemId);
            _itemDetailText.text = MapToPresentation((Weapon)item);
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void HideItemDetailView()
        {
            gameObject.SetActive(false);
        }

        private string MapToPresentation(Weapon weapon)
        {
            return $"Fire Rate: {weapon.FireRate}\n" +
                   $"Spread Angle: {weapon.SpreadAngle}\n" +
                   $"Bullet Speed: {weapon.BulletSpeed}\n" +
                   $"Bullets Per Shot: {weapon.BulletsPerShot}\n" +
                   $"Bullet Time To Live: {weapon.BulletTimeToLive}\n" +
                   $"Damage: {weapon.Damage}\n" +
                   $"Penetrate Damage Falloff: {weapon.PenetrateDamageFalloff}\n" +
                   $"Ammo Type: {weapon.AmmoType}";
        }
    }
}