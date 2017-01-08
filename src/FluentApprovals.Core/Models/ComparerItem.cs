using System.Reflection;

namespace FluentApprovals.Core.Models
{
    public class ComparerItem
    {
        public MemberInfo Member { get; set; }

        public IObjectComparer Comparer { get; set; }

        public ComparerItem(MemberInfo memberInfo, IObjectComparer comparer = null)
        {
            Member = memberInfo;
            Comparer = comparer;
        }
    }

   

   
}