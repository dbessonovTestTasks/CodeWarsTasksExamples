using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeWarsTasksExamples.Solutions.BigNumbers
{
    public class Kata
    {
        public static string AddReverse(List<int> bigger, List<int> lesser)
        {
            var result = string.Empty;
            int additional = 0;
            for (int i = 0; i < bigger.Count(); i++)
            {
                var csum = lesser.Count() > i ? bigger[i] + lesser[i] + additional : bigger[i] + additional;
                if (csum >= 10)
                {
                    additional = 1;
                    csum -= 10;
                }
                else
                    additional = 0;
                result += csum;
            }
            return additional == 1 ? "1" + String.Join("", result.Reverse()) : String.Join("", result.Reverse());
        }

        public static string Add(string a, string b)
        {
            return a.Length > b.Length
                ? AddReverse(a.Reverse().Select(o => int.Parse(o.ToString())).ToList(),
                    b.Reverse().Select(o => int.Parse(o.ToString())).ToList())
                : AddReverse(b.Reverse().Select(o => int.Parse(o.ToString())).ToList(),
                    a.Reverse().Select(o => int.Parse(o.ToString())).ToList());
        }
    }
}
