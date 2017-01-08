using FluentApprovals.Core.Impl;
using NUnit.Framework;

namespace FluentApprovals.Core.Tests.ComparerTests.Flat
{

    [TestFixture]
    public class FlatModelComparerTests
    {

        [Test]
        public void TestCompareIdenticalModels()
        {
            var modelA = new FlatModel {IntValue = 5, StringValue = "Hello"};
            var modelB = new FlatModel { IntValue = 5, StringValue = "Hello" };

            var comparer = new ObjectComparerFactory().Create<FlatModel>().All().Build();

            var result = comparer.AreEqual(modelA, modelB);

            Assert.IsTrue(result);

        }

        [Test]
        public void TestCompareDifferentModels()
        {
            var modelA = new FlatModel { IntValue = 1, StringValue = "Hello" };
            var modelB = new FlatModel { IntValue = 5, StringValue = "Hello" };

            var comparer = new ObjectComparerFactory().Create<FlatModel>().All().Build();

            var result = comparer.AreEqual(modelA, modelB);

            Assert.IsFalse(result);

        }

    }
}