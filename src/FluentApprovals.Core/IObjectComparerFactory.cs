namespace FluentApprovals.Core
{
    public interface IObjectComparerFactory
    {
        /// <summary> Creates object comparer builder for <typeparam name="T"></typeparam> </summary>
        IObjectComparerBuilder<T> Create<T>();
    }
}