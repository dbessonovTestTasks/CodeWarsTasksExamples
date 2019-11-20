using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeWarsTasksExamples.Solutions.Skyscrapers
{
    public class Skyscrapers
    {

        private class Territory
        {
            private Cell[][] _cells;
            private int[] _possibleStateList;
            private List<Clue> _clues = new List<Clue>();
            private int _size;

            public Territory(int[] clues)
            {
                _size = clues.Length / 4;
                _cells = new Cell[_size][];
                for (int i = 0; i < _size; i++)
                {
                    _cells[i] = new Cell[_size];
                    for (int j = 0; j < _size; j++)
                        _cells[i][j] = new Cell(i, j, GetPossibleStates);
                }
                _possibleStateList = Enumerable.Range(1, _size).ToArray();
                var variants = GenerateVariants(_possibleStateList);
                for (int i = 0; i < clues.Length; i++)
                    _clues.Add(new Clue(GetData(i, clues.Length), clues[i], clues[GetPairIndex(i, clues.Length)], variants));
            }

            private List<int[]> GenerateVariants(IEnumerable<int> possibleStateList)
            {
                var variants = new List<int[]>();
                foreach (var value in possibleStateList)
                {
                    var newPossibleStateList = possibleStateList.Except(new List<int> { value });
                    if (newPossibleStateList.Any())
                        foreach (var shuffle in GenerateVariants(newPossibleStateList))
                        {
                            var variant = new List<int> { value };
                            variant.AddRange(shuffle);
                            variants.Add(variant.ToArray());
                        }
                    else
                        variants.Add(new[] { value });
                }
                return variants;
            }

            private int GetPairIndex(int main, int len)
            {
                var side = len / 4;
                return (main + (side - main % side) * 2 + side - 1) % len;
            }

            private List<int> GetPossibleStates(int row, int column)
            {
                var cPosLis = new HashSet<int>();

                for (int i = 0; i < _size; i++)
                {
                    cPosLis.Add(_cells[row][i]);
                    cPosLis.Add(_cells[i][column]);
                }

                var posStates = new List<int>();
                foreach (var state in _possibleStateList)
                    if (!cPosLis.Contains(state))
                        posStates.Add(state);

                return posStates;
            }

            private List<Cell> GetData(int cluePosition, int len)
            {
                var pos = cluePosition / (len / 4);
                var ind = cluePosition >= len / 2
                    ? len / 4 - 1 - cluePosition % (len / 4)
                    : cluePosition % (len / 4);
                var skyscrapers = pos == 0 || pos == 2 ? _cells.Select(row => row.First(o => o.Column == ind)) : _cells[ind];
                return pos == 1 || pos == 2 ? skyscrapers.Reverse().ToList() : skyscrapers.ToList();
            }

            public void Solve()
            {
                var orderedClues = _clues.OrderBy(o => o.PossibleVariantsCount).ToList();
                orderedClues.First().Solve(orderedClues.Skip(1).ToList());
            }

            internal int[][] ToArray()
            {
                var result = new int[_clues.Count / 4][];
                for (int i = 0; i < _clues.Count / 4; i++)
                {
                    result[i] = new int[_clues.Count / 4];
                    _cells[i].Select(o => o.Value).ToList().CopyTo(result[i]);
                }
                return result;
            }
        }

        private class Cell
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public int Value { get; private set; }
            private int _previousValue;

            private Func<int, int, List<int>> _getPossibleValues;

            public Cell(int row, int column, Func<int, int, List<int>> getPossibleValues)
            {
                Row = row;
                Column = column;
                _getPossibleValues = getPossibleValues;
            }

            public List<int> PossibleValues
            {
                get { return _getPossibleValues(Row, Column); }
            }

            public bool Set(int value)
            {
                if (Value != 0)
                    return Value == value;

                _previousValue = Value;
                Value = value;
                return true;
            }

            public void Restore()
            {
                Value = _previousValue;
            }

            public static implicit operator int(Cell val)
            {
                return val.Value;
            }

        }

        private class Clue
        {
            private List<Cell> _cells;
            public int MainClue { get; private set; }
            public int PairClue { get; private set; }
            private List<int[]> _possibleVariants;

            public int PossibleVariantsCount
            {
                get { return _possibleVariants.Count; }
            }

            public Clue(List<Cell> cells, int clue, int pairClue, IEnumerable<int[]> allVariants)
            {
                _cells = cells;
                MainClue = clue;
                PairClue = pairClue;
                _possibleVariants = allVariants.Where(o => CheckClue(o.ToArray()) && CheckPairClue(o.ToArray())).ToList();
            }

            private IEnumerable<int[]> GetVariants()
            {
                var possible = _cells.Select(cell => cell.PossibleValues).ToList();
                var tmp = possible.Count();
                foreach (var variant in _possibleVariants)
                {
                    bool allRight = true;
                    for (int i = 0; i < tmp && allRight; i++)
                        if ((_cells[i].Value != 0 && variant[i] != _cells[i].Value) ||
                            (_cells[i].Value == 0 && !possible[i].Contains(variant[i])))
                            allRight = false;
                    if (allRight)
                        yield return variant;
                }
            }

            public bool Solve(List<Clue> nextClues)
            {
                var alreadySetted = _cells.Where(o => o.Value != 0).Select(o => _cells.IndexOf(o)).ToList();
                if (alreadySetted.Count == _cells.Count)
                    return CheckClue() && CheckPairClue() && (!nextClues.Any() || nextClues.First().Solve(nextClues.Skip(1).ToList()));
                nextClues =
                    nextClues.OrderByDescending(o => o._cells.Count(cell => cell.Value != 0))
                             .ThenBy(o => o.PossibleVariantsCount).ToList();

                foreach (var variant in GetVariants())
                {
                    bool allRight = true;
                    for (int i = 0; i < variant.Length && allRight; i++)
                        if (!_cells[i].Set(variant[i]))
                            allRight = false;
                    if (allRight && nextClues.Any())
                        allRight = nextClues.First().Solve(nextClues.Skip(1).ToList());

                    if (allRight)
                        return true;
                    RestoreCellState(alreadySetted);
                }
                return false;
            }

            private void RestoreCellState(List<int> exceptList)
            {
                for (int i = 0; i < _cells.Count; i++)
                    if (!exceptList.Contains(i))
                        _cells[i].Restore();
            }

            private int GetVisible(int[] skys)
            {
                var max = 0;
                int visibleCount = 0;
                foreach (int t in skys)
                    if (t > max)
                    {
                        visibleCount++;
                        max = t;
                    }
                return visibleCount;
            }

            private bool CheckClue(int[] skys = null)
            {
                return MainClue == 0 || GetVisible(skys ?? _cells.Select(o => o.Value).ToArray()) == MainClue;
            }

            private bool CheckPairClue(int[] skys = null)
            {
                return PairClue == 0 || GetVisible((skys ?? _cells.Select(o => o.Value)).Reverse().ToArray()) == PairClue;
            }
        }

        public static int[][] SolvePuzzle(int[] clues)
        {
            var territory = new Territory(clues);
            territory.Solve();
            return territory.ToArray();
        }
    }
}
