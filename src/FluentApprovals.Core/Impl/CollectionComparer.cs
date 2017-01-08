using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentApprovals.Core.Models;

namespace FluentApprovals.Core.Impl
{
    public class CollectionComparer : ComparerBase
    {

        public CollectionComparer(ComparerItem item) : base(new List<ComparerItem>{ item}) { }

        public CollectionComparer(ICollection<ComparerItem> items) : base(items) { }

        protected override bool AreEqualImpl(object value, object otherValue)
        {
            var list = value as IEnumerable;
            if (list == null)
                throw new ArgumentException(nameof(value));

            var otherList = otherValue as IEnumerable;
            if (otherList == null)
                throw new ArgumentException(nameof(otherValue));

            var enumerator = list.GetEnumerator();
            var otherEnumerator = otherList.GetEnumerator();

            bool moveNext = false;
            bool otherMoveNext = false;

            var itemComparer = _Items.First().Comparer;

            for (;;)
            {
                moveNext = enumerator.MoveNext();
                otherMoveNext = otherEnumerator.MoveNext();
                if (!moveNext || !otherMoveNext)
                    break;

                    if (!itemComparer.AreEqual(enumerator.Current, otherEnumerator.Current))
                        return false;
            }

            if (moveNext || otherMoveNext)
                return false;

            return true;
        }
    }
}