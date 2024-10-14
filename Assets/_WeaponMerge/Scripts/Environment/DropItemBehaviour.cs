using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;

namespace _WeaponMerge.Scripts.Environment
{
    public class DropItemBehaviour : MonoBehaviour
    {
        private Item _item;
        
        private void OnEnable()
        {
            _item = new WeaponsFactory().CreateWeapon(WeaponType.Pistol);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerPickUpBehaviour player))
            {
                player.PickUpItem(_item);
                ObjectPooler.Instance.ReturnToPool(DropType.Weapon, gameObject);
            }
        }
    }
}
