using System;

namespace Random.Stuff
{
    public static class RandomExtensions
    {
        private const double NormalizationFactor = 1.0 / int.MaxValue;

        public static ulong NextULong(this IRandom random, ulong min, ulong max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), $"invalid range {min} - {max}");
            }
            
            var range = max - min;
            var result = random.NextULongWithModuloBiasFix(range);
            return result % range + min;
        }
        
        public static long NextLong(this IRandom random) => (long) random.NextULong();

        public static long NextLong(this IRandom random, long min, long max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), $"invalid range {min} - {max}");
            }

            var range = (ulong) (max - min);
            var result = random.NextULongWithModuloBiasFix(range);
            return (long) (result % range) + min;
        }
        
        public static int Next(this IRandom random, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), $"invalid range {min} - {max}");
            }

            var range = (long) max - min;

            return min + (int) (range * random.NextDouble());
        }
        
        public static double NextDouble(this IRandom random)
        {
            var sample = random.Next();
            return sample * NormalizationFactor;
        }

        public static void NextBytes(this IRandom random, byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            for (var i = 0; i < buffer.Length; i += sizeof(ulong))
            {
                var length = Math.Min(buffer.Length - i, sizeof(ulong));
                var bytes = BitConverter.GetBytes(random.NextULong());
                Array.Copy(bytes, 0, buffer, i, length);
            }
        }

        private static ulong NextULongWithModuloBiasFix(this IRandom random, ulong range)
        {
            // Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
            // In the worst case, the expected number of calls is 2 (though usually it's much closer to 1) so this loop doesn't really hurt performance at all.
            var result = random.NextULong();
            var threshold = ulong.MaxValue - (ulong.MaxValue % range + 1) % range;
            while (result > threshold)
            {
                result = random.NextULong();
            }

            return result;
        }
    }
}