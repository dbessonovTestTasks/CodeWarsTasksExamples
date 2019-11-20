using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsTasksExamples.Solutions.Anagrams;
using NUnit.Framework;

namespace CodeWarsTasksExamples.Tests
{
    [TestFixture]
    public class KataTests
    {
        [TestCase("A", 1)]
        [TestCase("ABAB", 2)]
        [TestCase("AAAB", 1)]
        [TestCase("BAAA", 4)]
        [TestCase("QUESTION", 24572)]
        [TestCase("BOOKKEEPER", 10743)]
        public void TestNumberToOrdinal(string value, long expected)
        {
            Assert.AreEqual(expected, Kata.ListPosition(value), string.Format("Input {0}", value));
        }
    }
}
