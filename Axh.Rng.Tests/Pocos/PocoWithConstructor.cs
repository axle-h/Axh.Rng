using System;

namespace Random.Stuff.Tests.Pocos
{
    public class PocoWithConstructor
    {
        public PocoWithConstructor(long l, int i, short s, sbyte sByte, ulong uLong, uint uInt, ushort uShort, byte b,
            byte[] bytes, DayOfWeek @enum, Guid guid, DateTime dateTime, DateTimeOffset dateTimeOffset)
        {
            Long = l;
            Int = i;
            Short = s;
            SByte = sByte;
            ULong = uLong;
            UInt = uInt;
            UShort = uShort;
            Byte = b;
            Bytes = bytes;
            Enum = @enum;
            Guid = guid;
            DateTime = dateTime;
            DateTimeOffset = dateTimeOffset;
        }

        public long Long { get; }

        public int Int { get; }

        public short Short { get; }

        public sbyte SByte { get; }

        public ulong ULong { get; }

        public uint UInt { get; }

        public ushort UShort { get; }

        public byte Byte { get; }

        public byte[] Bytes { get; }

        public DayOfWeek Enum { get; }

        public Guid Guid { get; }

        public DateTime DateTime { get; }

        public DateTimeOffset DateTimeOffset { get; }
    }
}