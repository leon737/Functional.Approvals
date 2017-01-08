using System.Collections.Generic;
using FluentApprovals.Core.Models;

namespace FluentApprovals.Core.Impl
{
    public class ValueComparer : ComparerBase
    {

        public ValueComparer(ICollection<ComparerItem> items) : base(items) { }

        public ValueComparer(ComparerItem item) : base(new List<ComparerItem> {item}) { }

        protected override bool AreEqualImpl(object value, object otherValue) => value.Equals(otherValue);
    }
}