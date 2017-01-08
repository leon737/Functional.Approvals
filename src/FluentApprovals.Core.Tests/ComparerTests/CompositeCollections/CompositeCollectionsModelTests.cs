using System.Collections.Generic;
using FluentApprovals.Core.Impl;
using FluentApprovals.Core.Tests.ComparerTests.Collections;
using FluentApprovals.Core.Tests.ComparerTests.Composite;
using NUnit.Framework;

namespace FluentApprovals.Core.Tests.ComparerTests.CompositeCollections
{
    [TestFixture]
    public class CompositeCollectionsModelTests
    {
        protected IObjectComparer GetComparer()
        {
            var factory = new ObjectComparerFactory();
            return factory.Create<CompositeCollectionsModel>().All().Build();
        }

        [Test]
        public void TestCompareIdenticalModels()
        {
            var modelA = new CompositeCollectionsModel
            {
                IntValue = 5,
                CollectionValue = new List<Submodel>
            {
                new Submodel { StringValue = "Hello"},
                new Submodel { StringValue = "world"}
            }
            };
            var modelB = new CompositeCollectionsModel
            {
                IntValue = 5,
                CollectionValue = new List<Submodel>
            {
                new Submodel { StringValue = "Hello"},
                new Submodel { StringValue = "world"}
            }
            };
            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestCompareDifferentModels()
        {
            var modelA = new CompositeCollectionsModel
            {
                IntValue = 5,
                CollectionValue = new List<Submodel>
            {
                new Submodel { StringValue = "Hello"},
                new Submodel { StringValue = "world"}
            }
            };
            var modelB = new CompositeCollectionsModel
            {
                IntValue = 5,
                CollectionValue = new List<Submodel>
            {
                new Submodel { StringValue = "Bye"},
                new Submodel { StringValue = "world"}
            }
            };

            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsFalse(result);
        }
    }
}