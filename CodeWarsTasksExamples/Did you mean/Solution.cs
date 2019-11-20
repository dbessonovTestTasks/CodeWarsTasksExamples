using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWarsTasksExamples.Solutions.MostSimilar
{
    public class Kata
    {
        private IEnumerable<string> words;

        public Kata(IEnumerable<string> words)
        {
            this.words = words;
        }

        private IOrderedEnumerable<string> GetSubTerms(string term)
        {
            var subterms = new List<string>();
            for (int i = 0; i < term.Length; i++)
                for (int j = term.Length; j > i; j--)
                    subterms.Add(term.Substring(i, j - i));
            return subterms.OrderByDescending(s => s.Length);
        }

        private string RemoveIdentical(string word, string term)
        {
            if (word == term)
                return String.Empty;
            var basis = GetSubTerms(term).FirstOrDefault(word.Contains);
            var fromBasis = String.Empty;
            if (!string.IsNullOrWhiteSpace(basis))
                fromBasis = RemoveIdentical(word.Substring(0, word.IndexOf(basis)), term.Substring(0, term.IndexOf(basis))) +
                    RemoveIdentical(word.Substring(word.IndexOf(basis) + basis.Length), term.Substring(term.IndexOf(basis) + basis.Length));

            return string.IsNullOrWhiteSpace(fromBasis) || (fromBasis.Length > word.Length && fromBasis.Length > term.Length)
                        ? word.Length > term.Length ? word : term
                        : fromBasis;
        }

        public string FindMostSimilar(string term)
        {
            return words.ToDictionary(word => word, word => RemoveIdentical(word, term).Length)
                        .OrderBy(pair => pair.Value).First().Key;
        }
    }
}
