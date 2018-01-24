using System.Collections.Generic;

namespace Random.Stuff.Tests.Pocos
{
    public class PocoWithCollections
    {
        public string[] ArrayOfStrings { get; set; }

        public IEnumerable<string> EnumerableOfStrings { get; set; }

        public ICollection<string> CollectionOfStrings { get; set; }

        public IDictionary<string, string> DictionaryOfStrings { get; set; }

        public Dictionary<string, string> ConcreteDictionaryOfStrings { get; set; }

        public IList<string> ListOfStrings { get; set; }

        public List<string> ConcreteListOfStrings { get; set; }
    }
}
