namespace _WeaponMerge.Scripts.Inventory
{
    public class GetInventoryItemsUseCase
    {
        private readonly InventoryRepository _inventoryRepository;
        
        public GetInventoryItemsUseCase(InventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        
        public Item[] Execute()
        {
            return _inventoryRepository.GetItems();
        }
    }
}