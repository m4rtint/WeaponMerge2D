using _WeaponMerge.Scripts.Inventory;
using _WeaponMerge.Scripts.UserInterface.CoreUI.Data;
using _WeaponMerge.Scripts.Weapons;
using _WeaponMerge.Tools;

namespace _WeaponMerge.Scripts.UserInterface.CoreUI.Domain.UseCases
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
            var (item1, item2) = _repository.GetMergingItems();
            if (item1 is not Weapon weaponOne || item2 is not Weapon weaponTwo)
            {
                Log(item1, item2);
                return;
            }

            Logger.Log("Successfully Merged", LogKey.Merge);
            var weapon = _weaponMergeSystem.MergePistols(weaponOne, weaponTwo);
            _repository.AddMergedItemToInventory(weapon);
            _repository.SyncInventory();
        }

        private void Log(Item item1, Item item2)
        {
            if (item1 == null || item2 == null)
            {
                Logger.Log("Less than 2 items to merge", LogKey.Merge);
                return;
            }

            if (item1 is not Weapon)
            {
                Logger.Log("First item is not a weapon", LogKey.Merge);
            }

            if (item2 is not Weapon)
            {
                Logger.Log("Second item is not a weapon", LogKey.Merge);
            }
        }
    }
}