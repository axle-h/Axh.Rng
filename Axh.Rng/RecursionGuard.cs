using System;
using System.Linq;

namespace Axh.Rng
{
    internal class RecursionGuard
    {
        private readonly Type[] _typeStack;

        public RecursionGuard()
        {
            _typeStack = Array.Empty<Type>();
        }

        public RecursionGuard(RecursionGuard previous, Type type)
        {
            var previousStackLength = previous._typeStack.Length;
            _typeStack = new Type[previousStackLength + 1];
            Array.Copy(previous._typeStack, _typeStack, previousStackLength);
            _typeStack[previousStackLength] = type;
        }

        public bool TypeInRecursionStack(Type type) => _typeStack.Contains(type);
    }
}
