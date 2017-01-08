using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentApprovals.Core
{

    public interface IObjectComparerBuilder
    {
        /// <summary> Includes all public properties and fields </summary>
        IObjectComparerBuilder All();

        /// <summary> builds comparer </summary>
        IObjectComparer Build();
    }

    public interface IObjectComparerBuilder<T> : IObjectComparerBuilder
    {
        /// <summary> Includes all public properties and fields </summary>
        IObjectComparerBuilder<T> All();

        /// <summary> Excludes all public properties and fields </summary>
        IObjectComparerBuilder<T> None();

        /// <summary> Includes specified public property or field </summary>
        IObjectComparerBuilder<T> Include(Expression<Func<T, object>> memberSelector);

        /// <summary> Includes specified public property or field with custom comparer </summary>
        IObjectComparerBuilder<T> Include<V>(Expression<Func<T, V>> memberSelector, IObjectComparer comparer);

        /// <summary> Includes specified public property or field of IEnumerable{V} with custom comparer </summary>
        IObjectComparerBuilder<T> Include<V>(Expression<Func<T, IEnumerable<V>>> collectionSelector, IObjectComparer comparer);
        
        /// <summary> Excludes specified public property or field </summary>
        IObjectComparerBuilder<T> Exclude(Expression<Func<T, object>> memberSelector);
    }
}
