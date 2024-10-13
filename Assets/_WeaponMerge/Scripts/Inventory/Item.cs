namespace _WeaponMerge.Scripts.Inventory
{
    public abstract class Item
    {
        public readonly int Id;
        public readonly string Name;

        protected Item(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}