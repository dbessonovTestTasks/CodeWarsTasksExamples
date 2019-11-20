using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeWarsTasksExamples.Solutions.Runes
{
    public class Runes
    {
        private static Dictionary<char, Func<int, int, int>> _opDictionary = new Dictionary
            <char, Func<int, int, int>>
            {
                {'-', (i, i1) => i - i1},
                {'+', (i, i1) => i + i1},
                {'*', (i, i1) => i * i1}
            };

        private static int GetSignIndex(string expression)
        { return new Regex(@"(-|\+|\*)").Match(expression, 1).Index; }

        public static int solveExpression(string expression)
        {
            var digitList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var allowedDigits =
                digitList.Except(new Regex(@"[^0-9]").Replace(expression, "").Select(o => int.Parse(o.ToString())));

            var combine = expression.Split('=').First();
            var result = expression.Split('=').Last();
            var firstNum = combine.Substring(0, GetSignIndex(combine));
            var operation = combine[GetSignIndex(combine)];
            var secondNum = combine.Substring(GetSignIndex(combine) + 1);

            if ((firstNum[0] == '?' && firstNum.Length != 1) || (secondNum[0] == '?' && secondNum.Length != 1) ||
                (result[0] == '?' && result.Length != 1))
                allowedDigits = allowedDigits.Except(new[] { 0 });

            return Solve(allowedDigits, firstNum, operation, secondNum, result);
        }

        private static int Solve(IEnumerable<int> allowedDigits, string firstNum, char operation, string secondNum,
            string result)
        {

            foreach (
                var digit in
                    allowedDigits.Where(
                        digit => _opDictionary[operation](int.Parse(firstNum.Replace("?", digit.ToString())),
                            int.Parse(secondNum.Replace("?", digit.ToString()))) ==
                                 int.Parse(result.Replace("?", digit.ToString()))))
                return digit;

            return -1;
        }
    }
}
