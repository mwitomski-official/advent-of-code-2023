using AdventOfCode2023.App.Services;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Day3
{
    public partial class GearRatioManager
    {
        [GeneratedRegex(@"\d+")]
        private static partial Regex NumbersRegex();

        private string[] Lines { get; set; }
        public GearRatioManager()
        {
            Lines ??= FileManger.Read("Day3\\Data.txt");
        }

        // TEMP to Analyze | It's not mine solution!
        public int SolvePart2()
        {
            return Lines.Select((l, i) =>
                new Regex(@"\*").Matches(l).SelectMany(m => m.Groups.Cast<Group>().Select(g => g.Captures[0]))
                    .Select(g => new (int, int)[] { (i - 1, g.Index), (i + 1, g.Index) }
                        .Concat(Enumerable.Range(-1, 3).Select(j => (i + j, g.Index - 1))).Concat(Enumerable.Range(-1, 3).Select(j => (i + j, g.Index + 1)))
                        .Where(c => c.Item1 >= 0 && c.Item1 < Lines.Length && c.Item2 >= 0 && c.Item2 < Lines[c.Item1].Length)
                        .Where(i => Lines[i.Item1][i.Item2] >= '0' && Lines[i.Item1][i.Item2] <= '9')
                        .Select(n => (n.Item1, n.Item2,
                            string.Concat(Lines[n.Item1][..n.Item2].Reverse().TakeWhile(c => '0' <= c && c <= '9').Reverse()), string.Concat(Lines[n.Item1][(n.Item2 + 1)..].TakeWhile(c => '0' <= c && c <= '9'))))
                        .Select(t => (t.Item1, t.Item2, t.Item3, t.Item4, t.Item2 - t.Item3.Length, t.Item2 + t.Item4.Length))
                        .DistinctBy(t => (t.Item1, t.Item5, t.Item6))
                        .Select(t => t.Item3 + Lines[t.Item1][t.Item2] + t.Item4)
                        .Select(int.Parse)
                        .ToArray()
                    )
                    .Where(g => g.Length == 2)
                    .Sum(g => g.Aggregate((acc, v) => acc * v))
                ).Sum();
        }


        public int SolveEngineSchematic()
        {
            // Select and flatten all numbers with adjacent symbols, then sum their values
            return Lines
            .SelectMany((line, rowIndex)
                => ExtractNumbersWithAdjacentSymbols(line, Lines, rowIndex))
            .Sum(number => int.Parse(number.Value));
        }

        public int SolveImprovedEngineSchematic()
        {
            return SolvePart2();
        }

        // ------ HELPER --------

        private IEnumerable<Match> ExtractNumbersWithAdjacentSymbols(string line, string[] lines, int rowIndex)
        {
            // Use a regex to find all numbers in the line and filter those with adjacent symbols
            return NumbersRegex().Matches(line).Where(number => HasAdjacentSymbol(number, rowIndex, lines));
        }

        private bool HasAdjacentSymbol(Match number, int rowIndex, string[] lines)
        {
            // ----- Check if the number has adjacent symbols in the neighboring lines -----
            
            // Generate indices for the cells in the surrounding area of the current number
            var cellIndices = Enumerable.Range(-1, 3)
                .SelectMany(j => Enumerable.Range(-1, number.Length + 2)
                    .Select(k => (row: rowIndex + j, col: number.Index + k)));

            // Filter out invalid cell indices
            var validCellIndices = cellIndices.Where(cell => IsValidCell(cell, lines));

            // Extract symbols from the valid cell indices
            var symbols = validCellIndices.Select(cell => lines[cell.row][cell.col]);

            // Check if any symbol is not a period and is not a digit
            return symbols.Any(symbol => symbol != '.' && !char.IsDigit(symbol));
        }

        // Helper method to extract gear ratios for a specific '*' symbol
        private static int[] GetGearRatios(Match match, int rowIndex, string[] lines)
        {
            // Generate cell indices for the surrounding area of the '*' symbol
            var cellIndices = Enumerable.Range(-1, 3)
                .SelectMany(j => Enumerable.Range(-1, 3)
                    .Select(k => (rowIndex + j, match.Index + k)));

            // Filter out invalid cell indices
            var validCellIndices = cellIndices.Where(cell => IsValidCell(cell, lines));

            // Extract gear ratios from valid cell indices
            var gearRatios = validCellIndices.Select(cell => ExtractGearRatio(cell, lines));

            // Return distinct gear ratios
            return gearRatios.Distinct().ToArray();
        }

        // Helper method to extract a gear ratio from a specific cell
        private static int ExtractGearRatio((int row, int column) cell, string[] lines)
        {
            // Extract the gear ratio from the surrounding numbers
            var gearMatch = NumbersRegex().Match(lines[cell.row][cell.column] + lines[cell.row][(cell.column + 1)..]);

            // Parse the gear ratio
            if (gearMatch.Success)
                return int.Parse(gearMatch.Value);

            return 0;
        }

        /// <summary>
        /// Helper method to check if a cell is valid
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static bool IsValidCell((int row, int column) cell, string[] lines)
        {
            // Check if the cell coordinates are within the bounds of the lines array
            return cell.row >= 0 && cell.row < lines.Length && cell.column >= 0 && cell.column < lines[cell.row].Length;
        }
    }
}
