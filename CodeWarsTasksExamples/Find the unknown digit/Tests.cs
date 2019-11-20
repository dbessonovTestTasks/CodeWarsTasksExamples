using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsTasksExamples.Solutions.Runes;
using NUnit.Framework;

namespace CodeWarsTasksExamples.Tests
{    
    [TestFixture]
    public class RunesTest
    {
        [Test]
        public void testSample()
        {
            Assert.AreEqual(2, Runes.solveExpression("1+1=?"), "Answer for expression '1+1=?' ");
            Assert.AreEqual(6, Runes.solveExpression("123*45?=5?088"), "Answer for expression '123*45?=5?088' ");
            Assert.AreEqual(0, Runes.solveExpression("-5?*-1=5?"), "Answer for expression '-5?*-1=5?' ");
            Assert.AreEqual(-1, Runes.solveExpression("19--45=5?"), "Answer for expression '19--45=5?' ");
            Assert.AreEqual(5, Runes.solveExpression("??*??=302?"), "Answer for expression '??*??=302?' ");
            Assert.AreEqual(2, Runes.solveExpression("?*11=??"), "Answer for expression '?*11=??' ");
            Assert.AreEqual(2, Runes.solveExpression("??*1=??"), "Answer for expression '??*1=??' ");
            Assert.AreEqual(-1, Runes.solveExpression("??+??=??"), "Answer for expression '??+??=??' ");
        }
    }
}
