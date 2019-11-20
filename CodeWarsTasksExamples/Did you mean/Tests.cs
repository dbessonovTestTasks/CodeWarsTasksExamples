using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsTasksExamples.Solutions.MostSimilar;
using NUnit.Framework;

namespace CodeWarsTasksExamples.Tests
{
    [TestFixture]
    public class KataTest
    {
        [Test]
        public void TestDictionary1()
        {
            Kata kata = new Kata(new List<string> { "cherry", "pineapple", "melon", "strawberry", "raspberry" });
            Assert.AreEqual("strawberry", kata.FindMostSimilar("strawbery"));
            Assert.AreEqual("cherry", kata.FindMostSimilar("berry"));
        }

        [Test]
        public void TestDictionary2()
        {
            Kata kata = new Kata(new List<string> { "javascript", "java", "ruby", "php", "python", "coffeescript" });
            Assert.AreEqual("java", kata.FindMostSimilar("heaven"));
            Assert.AreEqual("javascript", kata.FindMostSimilar("javascript"));
        }
    }
}
