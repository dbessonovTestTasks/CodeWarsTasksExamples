using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeWarsTasksExamples.Solutions.Magnet
{
    public class Magnets
    {
        public static double Doubles(int maxk, int maxn)
        {
            double result = 0;
            for (int k = 1; k <= maxk; k++)
                for (int n = 1; n <= maxn; n++)
                    result += 1 / (k * Math.Pow(1 + n, 2 * k));
            return result;
        }
    }
}
