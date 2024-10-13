using UnityEngine.UI;

namespace _WeaponMerge.Scripts.Inventory
{
    public abstract class Item
    {
        public readonly int Id;
        public readonly string Name;
        public readonly Image Image;

        protected Item(int id, string name, Image image)
        {
            Id = id;
            Name = name;
            Image = image;
        }

        protected Item(int id, string name)
        {
            Id = id;
            Name = name;
        } 
    }
}