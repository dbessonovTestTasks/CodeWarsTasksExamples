using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeWarsTasksExamples.Solutions.Anagrams
{
    public class Kata
    {
        private static Dictionary<string, long> _maskDictionary = new Dictionary<string, long>();

        private static long CombinationsCount(string sorted, string word = null)
        {
            long num = 0;
            if (sorted == word)
                return 1;
            if (sorted.Count() == 2)
                return sorted[0] == sorted[1] ? 1 : 2;

            for (int i = 0; i < sorted.Count() && ((word != null && sorted[i] != word[0]) || word == null); i++)
                if (i == 0 || sorted[i - 1] != sorted[i])
                {
                    var substring = sorted.Substring(0, i) + sorted.Substring(i + 1, sorted.Length - i - 1);
                    var changeMask = GetMask(substring);
                    if (!_maskDictionary.ContainsKey(changeMask))
                        _maskDictionary.Add(changeMask, CombinationsCount(substring));
                    num += _maskDictionary[changeMask];
                }

            return word == null
                ? num
                : num + CombinationsCount(String.Join("", word.Substring(1).OrderBy(o => o)), word.Substring(1));
        }

        private static string GetMask(string substring)
        {
            var bAr = string.Empty;
            for (int i = 1; i < substring.Length; i++)
                bAr += substring[i - 1] == substring[i] ? 0 : 1;
            return bAr;
        }

        public static long ListPosition(string value)
        {
            return CombinationsCount(String.Join("", value.OrderBy(o => o)), value);
        }
    }
}
