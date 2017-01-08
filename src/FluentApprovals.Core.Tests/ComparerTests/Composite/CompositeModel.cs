namespace FluentApprovals.Core.Tests.ComparerTests.Composite
{
    public class CompositeModel
    {
        public int IntValue { get; set; }

        public Submodel SubmodelValue { get; set; }
    }

    public class Submodel
    {
        public string StringValue { get; set; }
    }
}