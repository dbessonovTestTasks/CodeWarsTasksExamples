using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWarsTasksExamples.Solutions.NextBigger
{
    public class Kata
    {
        private static string GetMinMax(string n)
        {
            var newN = n.Select(o => int.Parse(o.ToString())).ToList();
            var bigger = newN.Where(o => o > newN[0]);
            if (bigger.Any())
            {
                var newFirst = bigger.Min();
                if (newFirst != int.Parse(n[0].ToString()))
                {
                    newN.Remove(newFirst);
                    return newFirst + String.Join("", newN.OrderBy(o => o));
                }
            }
            return n;
        }

        public static long NextBiggerNumber(long n)
        {
            var ns = n.ToString();
            for (int i = ns.Length - 2; i >= 0; i--)
            {
                var new_ns = ns.Substring(0, i) + GetMinMax(ns.Substring(i));
                if (new_ns != ns)
                    return long.Parse(new_ns);
            }
            return -1;
        }
    }
}
