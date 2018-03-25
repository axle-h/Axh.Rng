using System;
using System.Linq;
using System.Reflection;

namespace Random.Stuff
{
    public static class Rng
    {
        private static readonly XorShiftRandom Random = new XorShiftRandom();

        public static byte[] Bytes(int length) => InRandomLock(r =>
        {
            var buffer = new byte[length];
            r.NextBytes(buffer);
            return buffer;
        });

        public static long Long(long min = long.MinValue, long max = long.MaxValue) =>
            InRandomLock(r => min == long.MinValue && max == long.MaxValue ? r.NextLong() : r.NextLong(min, max));

        public static ulong UnsignedLong(ulong min = ulong.MinValue, ulong max = ulong.MaxValue) =>
            InRandomLock(r => min == ulong.MinValue && max == ulong.MaxValue ? r.NextULong() : r.NextULong(min, max));

        public static int Int(int min = int.MinValue, int max = int.MaxValue) => InRandomLock(r => r.Next(min, max));

        public static uint UnsignedInt(uint min = uint.MinValue, uint max = uint.MaxValue) => unchecked((uint) UnsignedLong(min, max));

        public static short Short(short min = short.MinValue, short max = short.MaxValue) => unchecked((short) Int(min, max));

        public static ushort UnsignedShort(ushort min = ushort.MinValue, ushort max = ushort.MaxValue) => unchecked((ushort) UnsignedLong(min, max));

        public static sbyte SByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue) => unchecked((sbyte) Int(min, max));

        public static byte Byte(byte min = byte.MinValue, byte max = byte.MaxValue) => unchecked((byte) Int(min, max));

        public static TEnum Enum<TEnum>()
        {
            var type = typeof(TEnum);
            if (!type.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Must be an enumerated type", nameof(type));
            }

            var values = System.Enum.GetValues(type).Cast<TEnum>().ToArray();
            return Pick(values);
        }

        public static Guid Guid() => System.Guid.NewGuid();

        public static DateTime DateTime() => System.DateTime.UtcNow;

        public static DateTimeOffset DateTimeOffset() => System.DateTimeOffset.UtcNow;

        public static string String() => Guid().ToString();

        public static TElement Pick<TElement>(params TElement[] elements)
        {
            if (!elements.Any())
            {
                throw new ArgumentException("no elements to pick", nameof(elements));
            }
            return elements[Int(0, elements.Length)];
        }

        public static Func<TObject> ObjectFactory<TObject>() => RngFactory.Build<TObject>();

        private static TObject InRandomLock<TObject>(Func<XorShiftRandom, TObject> f)
        {
            lock (Random)
            {
                return f(Random);
            }
        }
    }
}
