using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentApprovals.Core.Models;
using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;
using Functional.Fluent.Pattern;

namespace FluentApprovals.Core.Impl
{
    public class ObjectComparerBuilder<T> : IObjectComparerBuilder<T>
    {
        protected Type _Type;

        protected IDictionary<MemberInfo, ComparerItem> _MemberInfos;

        internal ObjectComparerBuilder()
        {
            _Type = typeof(T);
        }

        /// <summary> Includes all public properties and fields </summary>
        public IObjectComparerBuilder<T> All() => Self(() =>
            _MemberInfos = GetAllMembers().ToDictionary(v => v, v => new ComparerItem(v, GetComparer(v))));


        /// <summary> Includes all public properties and fields </summary>
        IObjectComparerBuilder IObjectComparerBuilder.All() => All();


        /// <summary> Excludes all public properties and fields </summary>
        public IObjectComparerBuilder<T> None() => Self(() =>
            _MemberInfos = new Dictionary<MemberInfo, ComparerItem>());


        /// <summary> Includes specified public property or field </summary>
        public IObjectComparerBuilder<T> Include(Expression<Func<T, object>> memberSelector) => Self(() =>
            AddMember(GetMember(memberSelector)));


        /// <summary> Includes specified public property or field with custom comparer </summary>
        public IObjectComparerBuilder<T> Include<V>(Expression<Func<T, V>> memberSelector, IObjectComparer comparer) => Self(() =>
            AddMember(GetMember(memberSelector), comparer));

        /// <summary> Includes specified public property or field of IEnumerable{V} with custom comparer </summary>
        public IObjectComparerBuilder<T> Include<V>(Expression<Func<T, IEnumerable<V>>> collectionSelector,
            IObjectComparer comparer) => Self(() =>
                GetMember(collectionSelector).ToM()
                .Do(m => AddMember(m, new CollectionComparer(new ComparerItem(m, comparer)))));

        /// <summary> Excludes specified public property or field </summary>
        public IObjectComparerBuilder<T> Exclude(Expression<Func<T, object>> memberSelector) => Self(() =>
            RemoveMember(GetMember(memberSelector)));

        /// <summary> Builds comparer </summary>
        public IObjectComparer Build() => new ObjectComparer(_MemberInfos.Values);

        protected IEnumerable<MemberInfo> GetAllMembers() =>
            _Type.GetMembers().Where(x => x.MemberType == MemberTypes.Property || x.MemberType == MemberTypes.Field);

        protected void AddMember(MemberInfo memberInfo, IObjectComparer comparer = null) => 
            _MemberInfos[memberInfo] = new ComparerItem(memberInfo, comparer ?? GetComparer(memberInfo));

        protected void RemoveMember(MemberInfo memberInfo)
        {
            if (_MemberInfos.ContainsKey(memberInfo))
                _MemberInfos.Remove(memberInfo);
        }

        protected IObjectComparer GetComparer(MemberInfo memberInfo) => GetComparer(memberInfo.TypeMatch()
            .With(Case.Is<PropertyInfo>(), v => v.PropertyType)
            .With(Case.Is<FieldInfo>(), v => v.FieldType)
            .Do(), memberInfo);

        protected IObjectComparer GetComparer(Type type, MemberInfo memberInfo) => type.Match()
            .With(x => x.IsPrimitive || x == typeof(string), _ => (IObjectComparer)new ValueComparer(new ComparerItem(memberInfo)))
            .With(x => typeof(IEnumerable).IsAssignableFrom(x), _ => new CollectionComparer(new ComparerItem(memberInfo,
                GetItemType(type).With(t => GetComparer(t, null)).IsNull(new ValueComparer(new ComparerItem(memberInfo))).Value)))
            .Else(_ => ((IObjectComparerBuilder) typeof(ObjectComparerBuilder<>).MakeGenericType(type)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {}, null)
                .Invoke(BindingFlags.NonPublic, null, null, CultureInfo.CurrentCulture)).All().Build()).Do();

        protected MemberInfo GetMember<V>(Expression<Func<T, V>> memberSelector)
        {
            var expression = memberSelector.Body as MemberExpression;
            if (expression == null)
                throw new ArgumentException(nameof(memberSelector));
            return expression.Member;
        }

        protected IObjectComparerBuilder<T> Self(Action action)
        {
            action();
            return this;
        }

        private Maybe<Type> GetItemType(Type type) => type.GenericTypeArguments.FirstOrDefault();
    }
}