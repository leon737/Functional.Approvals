using FluentApprovals.Core.Impl;
using FluentApprovals.Core.Models;
using NUnit.Framework;

namespace FluentApprovals.Core.Tests.ComparerTests
{

    [TestFixture]
    public class CollectionComparerTests
    {
        [Test]
        public void TestCompareDifferentLengthCollections()
        {
            var value = new int[] {1, 2, 3, 4, 5};
            var otherValue = new int[] { 1, 2, 3, 4, 5, 6 };
            var comparer = new CollectionComparer(new ComparerItem(null, new ValueComparer((ComparerItem)null)));
            Assert.IsFalse(comparer.AreEqual(value, otherValue));
        }


        [Test]
        public void TestCompareIdenticalCollections()
        {
            var value = new int[] { 1, 2, 3, 4, 5 };
            var otherValue = new int[] { 1, 2, 3, 4, 5 };
            var comparer = new CollectionComparer(new ComparerItem(null, new ValueComparer((ComparerItem)null)));
            Assert.IsTrue(comparer.AreEqual(value, otherValue));
        }

        [Test]
        public void TestCompareDifferentCollections()
        {
            var value = new int[] { 1, 2, 6, 4, 5 };
            var otherValue = new int[] { 1, 2, 3, 4, 5 };
            var comparer = new CollectionComparer(new ComparerItem(null, new ValueComparer((ComparerItem)null)));
            Assert.IsFalse(comparer.AreEqual(value, otherValue));
        }




    }
}