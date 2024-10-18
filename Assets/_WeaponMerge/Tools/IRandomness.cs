using System;

namespace _WeaponMerge.Tools
{
    public interface IRandomness
    {
        int Range(int min, int max);
        float Range(float min, float max);
        bool CoinFlip();
    }

    public class Randomness: IRandomness
    {
        private readonly Random _random;
        
        public Randomness(int seed)
        {
            _random = new Random(seed);
        }
        
        public int Range(int min, int max)
        {
            return _random.Next(min, max);
        }
        
        public float Range(float min, float max)
        {
            return (float) (_random.NextDouble() * (max - min) + min);
        }
        
        public bool CoinFlip()
        {
            return _random.Next(0, 2) == 0;
        }
    }
}