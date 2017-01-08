using FluentApprovals.Core.Impl;
using NUnit.Framework;

namespace FluentApprovals.Core.Tests.ComparerTests.Composite
{

    [TestFixture]
    public class CompositeModelTests
    {

        protected IObjectComparer GetComparer()
        {
            var factory = new ObjectComparerFactory();
            return factory.Create<CompositeModel>().All().Build();
        }

        [Test]
        public void TestCompareIdenticalModels()
        {
            var modelA = new CompositeModel {IntValue = 5, SubmodelValue = new Submodel {StringValue = "Hello"}};
            var modelB = new CompositeModel { IntValue = 5, SubmodelValue = new Submodel { StringValue = "Hello" } };
            
            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestCompareDifferentModels()
        {
            var modelA = new CompositeModel { IntValue = 1, SubmodelValue = new Submodel { StringValue = "Hello" } };
            var modelB = new CompositeModel { IntValue = 5, SubmodelValue = new Submodel { StringValue = "Hello" } };

            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestCompareDifferentModels2()
        {
            var modelA = new CompositeModel { IntValue = 5, SubmodelValue = new Submodel { StringValue = "Hello" } };
            var modelB = new CompositeModel { IntValue = 5, SubmodelValue = new Submodel { StringValue = "Bye" } };

            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestCompareDifferentModelsExcludeSubmodelProperty()
        {
            var modelA = new CompositeModel { IntValue = 5, SubmodelValue = new Submodel { StringValue = "Hello" } };
            var modelB = new CompositeModel { IntValue = 5, SubmodelValue = new Submodel { StringValue = "Bye" } };

            var result = new ObjectComparerFactory().Create<CompositeModel>().All()
                .Include(v => v.SubmodelValue, 
                    new ObjectComparerFactory().Create<Submodel>().All().Exclude(x => x.StringValue).Build())
                .Build().AreEqual(modelA, modelB);

            Assert.IsTrue(result);
        }

    }
}