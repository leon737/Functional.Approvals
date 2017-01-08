using System.Collections.Generic;
using FluentApprovals.Core.Tests.ComparerTests.Composite;

namespace FluentApprovals.Core.Tests.ComparerTests.CompositeCollections
{
    public class CompositeCollectionsModel
    {
        public int IntValue { get; set; }

        public ICollection<Submodel> CollectionValue { get; set; }
    }
}