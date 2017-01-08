using System.Collections.Generic;
using FluentApprovals.Core.Impl;
using NUnit.Framework;

namespace FluentApprovals.Core.Tests.ComparerTests.Collections
{
    [TestFixture]
    public class CollectionsModelTests
    {
        protected IObjectComparer GetComparer()
        {
            var factory = new ObjectComparerFactory();
            return factory.Create<CollectionsModel>().All().Build();
        }

        [Test]
        public void TestCompareIdenticalModels()
        {
            var modelA = new CollectionsModel {IntValue = 5, CollectionValue = new List<int> {1, 2, 3, 4, 5}};
            var modelB = new CollectionsModel { IntValue = 5, CollectionValue = new List<int> { 1, 2, 3, 4, 5 } };

            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestCompareDifferentModels()
        {
            var modelA = new CollectionsModel { IntValue = 5, CollectionValue = new List<int> { 1, 2, 3, 4, 5 } };
            var modelB = new CollectionsModel { IntValue = 5, CollectionValue = new List<int> { 1, 2, 6, 4, 5 } };

            var result = GetComparer().AreEqual(modelA, modelB);

            Assert.IsFalse(result);
        }
    }
}