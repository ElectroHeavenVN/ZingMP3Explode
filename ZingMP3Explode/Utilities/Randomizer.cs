using System;
using System.Security.Cryptography;

namespace ZingMP3Explode.Utilities
{
    internal static class Randomizer
    {
        static readonly RandomNumberGenerator Generator = RandomNumberGenerator.Create();

        static Random Generate()
        {
            var buffer = new byte[4];
            Generator.GetBytes(buffer);
            return new Random(BitConverter.ToInt32(buffer, 0));
        }

        internal static Random Instance => _rand ??= Generate();
        
        [ThreadStatic] 
        static Random? _rand;
    }
}
