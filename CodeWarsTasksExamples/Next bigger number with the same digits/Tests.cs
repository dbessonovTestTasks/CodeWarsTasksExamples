using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsTasksExamples.Solutions.NextBigger;
using NUnit.Framework;

namespace CodeWarsTasksExamples.Tests
{
    [TestFixture]
    public class NextBiggerNumberTests
    {
        [Test]
        public void Test1()
        {
            Console.WriteLine("****** Small Number");
            Assert.AreEqual(21, Kata.NextBiggerNumber(12));
            Assert.AreEqual(531, Kata.NextBiggerNumber(513));
            Assert.AreEqual(2071, Kata.NextBiggerNumber(2017));
            Assert.AreEqual(441, Kata.NextBiggerNumber(414));
            Assert.AreEqual(414, Kata.NextBiggerNumber(144));
        }
    }
}
