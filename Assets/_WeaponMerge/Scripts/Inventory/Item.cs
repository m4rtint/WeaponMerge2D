using UnityEngine;

namespace _WeaponMerge.Scripts.Inventory
{
    public abstract class Item
    {
        public readonly int Id;
        public readonly string Name;
        public readonly Sprite Sprite;

        protected Item(int id, string name, Sprite sprite)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
        }

        protected Item(int id, string name)
        {
            Id = id;
            Name = name;
        } 
    }
}