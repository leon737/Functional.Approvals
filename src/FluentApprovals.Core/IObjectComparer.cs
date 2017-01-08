namespace FluentApprovals.Core
{
    public interface IObjectComparer
    {
        bool AreEqual(object value, object otherValue);
    }
    
}
