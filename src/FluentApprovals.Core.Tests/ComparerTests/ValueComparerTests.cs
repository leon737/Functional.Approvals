using FluentApprovals.Core.Impl;
using FluentApprovals.Core.Models;
using NUnit.Framework;

namespace FluentApprovals.Core.Tests.ComparerTests
{

    [TestFixture]
    public class ValueComparerTests
    {

        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        public void TestCompareIntValues(int value, int otherValue, bool expected)
        {
            var result = new ValueComparer((ComparerItem)null).AreEqual(value, otherValue);
            Assert.AreEqual(expected, result);
        }

        [TestCase("a", "a", true)]
        [TestCase("a", "b", false)]
        public void TestCompareStringValues(string value, string otherValue, bool expected)
        {
            var result = new ValueComparer((ComparerItem)null).AreEqual(value, otherValue);
            Assert.AreEqual(expected, result);
        }


    }
}