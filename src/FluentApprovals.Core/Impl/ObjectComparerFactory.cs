namespace FluentApprovals.Core.Impl
{
    public class ObjectComparerFactory : IObjectComparerFactory
    {
        /// <summary> Creates object comparer builder for <typeparam name="T"></typeparam> </summary>
        public IObjectComparerBuilder<T> Create<T>() => new ObjectComparerBuilder<T>();
    }
}