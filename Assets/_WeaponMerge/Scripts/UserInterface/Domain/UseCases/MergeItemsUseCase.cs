using _WeaponMerge.Scripts.UserInterface.Data;
using _WeaponMerge.Scripts.Weapons;

namespace _WeaponMerge.Scripts.UserInterface.Domain.UseCases
{
    public class MergeItemsUseCase
    {
        private readonly IMergeRepository _repository;
        private readonly WeaponMergingSystem _weaponMergeSystem;
        
        public MergeItemsUseCase(IMergeRepository repository, WeaponMergingSystem weaponMergeSystem)
        {
            _repository = repository;
            _weaponMergeSystem = weaponMergeSystem;
        }
        
        public void Execute()
        {
            var items = _repository.GetMergingItems();
            if (items.Length < 2 || items[0] is not Weapon weaponOne || items[1] is not Weapon weaponTwo)
            {
                return;
            }

            var weapon = _weaponMergeSystem.MergePistols(weaponOne, weaponTwo);
            _repository.AddMergedItemToInventory(weapon);
        }
    }
}