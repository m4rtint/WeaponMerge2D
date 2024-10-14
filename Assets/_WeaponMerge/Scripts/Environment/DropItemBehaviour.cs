using System;
using _WeaponMerge.Scripts.Characters.Players;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _WeaponMerge.Scripts.Environment
{
    public class DropItemBehaviour : MonoBehaviour
    {
        private Item _item;
        
        private void OnEnable()
        {
            _item = new Weapon(
                Random.Range(1000, 99999),
                "Pistol",
                1f,
                5f,
                10f,
                1,
                2f,
                10,
                0.5f,
                AmmoType.Pistol);
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
