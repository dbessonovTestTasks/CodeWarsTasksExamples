using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsTasksExamples.Solutions.BigNumbers;
using NUnit.Framework;

namespace CodeWarsTasksExamples.Tests
{
    [TestFixture]
    public class SolutionTestBigNum
    {
        [Test]
        public void MyTest()
        {
            Assert.AreEqual("444", Kata.Add("123", "321"));
        }
    }
}
