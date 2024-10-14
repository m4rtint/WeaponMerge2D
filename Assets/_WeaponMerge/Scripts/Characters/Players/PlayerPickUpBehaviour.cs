using _WeaponMerge.Scripts.Characters.Players.Domain.UseCases;
using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.Data;
using UnityEngine;

namespace _WeaponMerge.Scripts.Characters.Players
{
    public class PlayerPickUpBehaviour: MonoBehaviour
    {
        private PickUpItemUseCase _pickUpItemUseCase;
        private void Awake()
        {
            var inventoryRepository = new InventoryRepository(new InventoryStorage());
            _pickUpItemUseCase = new PickUpItemUseCase(inventoryRepository);
        }

        public void PickUpItem(Item item)
        {
            _pickUpItemUseCase.Execute(item);
        }
    }
}