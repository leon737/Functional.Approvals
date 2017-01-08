using System.Collections.Generic;

namespace FluentApprovals.Core.Tests.ComparerTests.Collections
{
    public class CollectionsModel
    {
        public int IntValue { get; set; }

        public ICollection<int> CollectionValue { get; set; }
    }
}