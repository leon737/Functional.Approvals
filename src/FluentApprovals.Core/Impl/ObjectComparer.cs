using System;
using System.Collections.Generic;
using System.Reflection;
using FluentApprovals.Core.Models;
using Functional.Fluent.Extensions;
using Functional.Fluent.Pattern;

namespace FluentApprovals.Core.Impl
{
    public class ObjectComparer : ComparerBase
    {

        public ObjectComparer(ICollection<ComparerItem> memberInfos) : base(memberInfos)
        {
        }

        protected override bool AreEqualImpl(object value, object otherValue)
        {
            foreach (var item in _Items)
            {
                var member = item.Member;
                var values = member.TypeMatch()
                    .With(Case.Is<PropertyInfo>(), v =>Tuple.Create(v.GetValue(value), v.GetValue(otherValue)))
                    .With(Case.Is<FieldInfo>(), v => Tuple.Create(v.GetValue(value), v.GetValue(otherValue)))
                    .Do();
                if (!item.Comparer.AreEqual(values.Item1, values.Item2))
                    return false;
            }
            return true;
        }
    }
}