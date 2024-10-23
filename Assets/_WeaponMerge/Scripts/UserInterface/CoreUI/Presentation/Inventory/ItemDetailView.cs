using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Presentation.Inventory
{
    public class ItemDetailView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText = null;
        [SerializeField] private TMP_Text _itemDetailText = null;
        [SerializeField] private TMP_Text _bulletDataText = null;
        [SerializeField] private Image _itemImage = null;
        
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
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                return;
            }

            _instance = this;

            _getInventoryItemUseCase = new GetInventoryItemUseCase(new InventoryRepository(new InventoryStorage()));
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
            _titleText.text = item.Name;
            _itemDetailText.text = MapToPresentation((Weapon)item);
            _bulletDataText.text = MapToBulletDataPresentation((Weapon)item);
            _itemImage.sprite = item.Sprite;
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void HideItemDetailView()
        {
            gameObject.SetActive(false);
        }

         private string MapToPresentation(Weapon weapon)
        {
            return $"{FormatNumber(weapon.Damage / weapon.FireRate)}\n" +
                   $"{FormatNumber(weapon.Damage)}\n" +
                   $"{FormatNumber(weapon.FireRate)}";
        }

        private string MapToBulletDataPresentation(Weapon weapon)
        {
            return $"{FormatNumber(weapon.SpreadAngle)}\n" +
                   $"{FormatNumber(weapon.BulletTimeToLive)}\n" +
                   $"{FormatNumber(weapon.BulletSpeed)}";
        }

        private string FormatNumber(float number)
        {
            return number.ToString("F2");
        }
    }
}