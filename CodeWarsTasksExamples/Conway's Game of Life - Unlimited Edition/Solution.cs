using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeWarsTasksExamples.Solutions.GameOfLife
{
    public class ConwayLife
    {
        private class Territory
        {
            private List<Cell> _cells = new List<Cell>();
            private List<Cell> _liveCells
            {
                get { return _cells.Where(o => o.State == 1).ToList(); }
            }

            private int MinRowIndex
            {
                get { return _liveCells.Select(o => o.Row).Min(); }
            }

            private int MaxRowIndex
            {
                get { return _liveCells.Select(o => o.Row).Max(); }
            }

            private int MinColumnIndex
            {
                get { return _liveCells.Select(o => o.Column).Min(); }
            }

            private int MaxColumnIndex
            {
                get { return _liveCells.Select(o => o.Column).Max(); }
            }

            public void Add(int i, int j, int state)
            { _cells.Add(new Cell(i, j, state, GetNeighboursCount)); }

            public void CheckBounds()
            {
                if (_liveCells.Any())
                {
                    AddRowIfNeed(MinRowIndex, _cells.Select(o => o.Row).Min(), -1);
                    AddRowIfNeed(MaxRowIndex, _cells.Select(o => o.Row).Max(), 1);
                    AddColumnIfNeed(MinColumnIndex, _cells.Select(o => o.Column).Min(), -1);
                    AddColumnIfNeed(MaxColumnIndex, _cells.Select(o => o.Column).Max(), 1);
                }
            }

            public void NextStep()
            {
                CheckBounds();
                foreach (var cell in _cells)
                    cell.ProcessNextState();
                foreach (var cell in _cells)
                    cell.ToNextState();
            }

            private void AddColumnIfNeed(int lifeColumn, int column, int direction)
            {
                if (lifeColumn == column)
                    for (int i = _cells.Select(o => o.Row).Min(); i <= _cells.Select(o => o.Row).Max(); i++)
                        _cells.Add(new Cell(i, lifeColumn + direction, 0, GetNeighboursCount));
            }

            private void AddRowIfNeed(int lifeRow, int row, int direction)
            {
                if (lifeRow == row)
                    for (int i = _cells.Select(o => o.Column).Min(); i <= _cells.Select(o => o.Column).Max(); i++)
                        _cells.Add(new Cell(lifeRow + direction, i, 0, GetNeighboursCount));
            }

            private int GetNeighboursCount(int row, int column)
            {
                return
                    _liveCells.Count(
                        o =>
                            o.Row >= row - 1 && o.Row <= row + 1 && o.Column >= column - 1 && o.Column <= column + 1 &&
                            !(o.Row == row && o.Column == column));
            }

            internal int[,] ToArray()
            {
                var result = new int[MaxRowIndex - MinRowIndex + 1, MaxColumnIndex - MinColumnIndex + 1];
                for (int i = 0; i < MaxRowIndex - MinRowIndex + 1; i++)
                    for (int j = 0; j < MaxColumnIndex - MinColumnIndex + 1; j++)
                        result[i, j] =
                            _cells.First(o => o.Row == i + MinRowIndex && o.Column == j + MinColumnIndex).State;
                return result;
            }
        }

        private class Cell
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int State { get; private set; }
            public int NextState { get; set; }
            private Func<int, int, int> _getNeighboursCount;

            public Cell(int row, int column, int state, Func<int, int, int> getNeighboursCount)
            {
                Row = row;
                Column = column;
                State = state;
                _getNeighboursCount = getNeighboursCount;
            }

            public void ProcessNextState()
            {
                switch (_getNeighboursCount(Row, Column))
                {
                    case 2:
                        NextState = State;
                        break;
                    case 3:
                        NextState = 1;
                        break;
                    default:
                        NextState = 0;
                        break;
                }
            }

            public void ToNextState()
            {
                State = NextState;
                NextState = -1;
            }
        }

        public static int[,] GetGeneration(int[,] cells, int generation)
        {
            var territory = new Territory();
            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                    territory.Add(i, j, cells[i, j]);
            for (int i = 0; i < generation; i++)
                territory.NextStep();
            return territory.ToArray();
        }

    }
}
