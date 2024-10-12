namespace _WeaponMerge.Scripts.Inventory
{
    public abstract class Item
    {
        private readonly int _id;
        private readonly string _name;

        protected Item(int id, string name)
        {
            _id = id;
            _name = name;
        }
    }
}