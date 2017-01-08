using System.Collections.Generic;
using FluentApprovals.Core.Models;

namespace FluentApprovals.Core.Impl
{
    public abstract class ComparerBase : IObjectComparer
    {

        protected ICollection<ComparerItem> _Items;

        protected ComparerBase(ICollection<ComparerItem> items)
        {
            _Items = items;
        }

        public bool AreEqual(object value, object otherValue)
        {
            if (value == null && otherValue == null) return true;

            if (value == null || otherValue == null) return false;

            if (ReferenceEquals(value, otherValue)) return true;

            return AreEqualImpl(value, otherValue);
        }

        protected abstract bool AreEqualImpl(object value, object otherValue);
    }
}