using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsTasksExamples.Solutions.GameOfLife;
using NUnit.Framework;

namespace CodeWarsTasksExamples.Tests
{
    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void TestGlider()
        {
            int[][,] gliders =
            {
        new int[,] {{1,0,0},{0,1,1},{1,1,0}},
        new int[,] {{0,1,0},{0,0,1},{1,1,1}}
      };
            Console.WriteLine("Glider");
            int[,] res = ConwayLife.GetGeneration(gliders[0], 1);
            CollectionAssert.AreEqual(gliders[1], res, "Output doesn't match expected");
        }
    }
}
