using System;

namespace Random.Stuff.Tests.Pocos
{
    public class SimplePoco
    {
        public long Long { get; set; }

        public int Int { get; set; }

        public short Short { get; set; }

        public sbyte SByte { get; set; }

        public ulong ULong { get; set; }

        public uint UInt { get; set; }

        public ushort UShort { get; set; }

        public byte Byte { get; set; }

        public byte[] Bytes { get; set; }

        public DayOfWeek Enum { get; set; }

        public Guid Guid { get; set; }

        public DateTime DateTime { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }
    }
}