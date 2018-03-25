using System;

namespace Random.Stuff
{
    public class XorShiftRandom : IRandom
    {
        private readonly ulong[] _state;
        private ulong _seed;
        private bool _takeMsb = true;
        private ulong _currentPartialResult;

        public XorShiftRandom(ulong seed)
        {
            _seed = seed;
            _state = new[] { SplitMix64(), SplitMix64() };
        }

        public XorShiftRandom() : this((ulong) DateTime.UtcNow.Ticks)
        {
        }

        public ulong NextULong() => XorShift128Plus();
        
        public int Next()
        {
            int sample;

            if (_takeMsb)
            {
                _currentPartialResult = XorShift128Plus();
                sample = unchecked((int) (_currentPartialResult >> 32));
            }
            else
            {
                sample = unchecked((int) _currentPartialResult);
            }

            _takeMsb = !_takeMsb;

            sample &= int.MaxValue;
            return sample == int.MaxValue ? --sample : sample;
        }

        private ulong SplitMix64()
        {
            var z = unchecked(_seed += 0x9E3779B97F4A7C15);
            z = unchecked((z ^ (z >> 30)) * 0xBF58476D1CE4E5B9);
            z = unchecked((z ^ (z >> 27)) * 0x94D049BB133111EB);
            return z ^ (z >> 31);
        }

        private ulong XorShift128Plus()
        {
            var s1 = _state[0];
            var s0 = _state[1];
            var result = s0 + s1;
            _state[0] = s0;
            s1 ^= s1 << 23;
            _state[1] = s1 ^ s0 ^ (s1 >> 18) ^ (s0 >> 5);
            return result;
        }
    }
}
