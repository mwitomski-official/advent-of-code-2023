using AdventOfCode2023.App.Extensions;
using AdventOfCode2023.App.Services;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Day1
{
    public partial class RecoveryManager
    {
        [GeneratedRegex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]")]
        private static partial Regex NumberRegex();
        [GeneratedRegex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]", RegexOptions.RightToLeft)]
        private static partial Regex NumberReversedRegex();

        public string[] Lines { get; set; }
        public enum Numbers
        {
            one = 1,
            two = 2,
            three = 3,
            four = 4,
            five = 5,
            six = 6,
            seven = 7,
            eight = 8,
            nine = 9
        }

        public RecoveryManager()
        {
          Lines ??= FileManger.Read("Day1\\Data.txt");
        }

        public int RunCalibration()
            => Lines
               .Select(l => string.Join("", l.Where(g => char.IsDigit(g))))
               .Sum(l => int.Parse(l.FirstOrDefault().ToString() + l.LastOrDefault().ToString()));

        public int RunSecondCalibration()
            => Lines
            .Select(line => GetNUmber(line))
            .Sum();

        private int GetNUmber(string line)
        {
            var numberRegex = NumberRegex();
            var numberRegexFromRight = NumberReversedRegex();

            string leftNumber = numberRegex
                .Match(line).Value;
            string rightNumber = numberRegexFromRight
                .Match(line).Value;
            
            if (leftNumber.Length != 1) 
                leftNumber = leftNumber
                    .ToDigit();

            if (rightNumber.Length != 1) 
                rightNumber = rightNumber
                    .ToDigit();

            return int.Parse($"{leftNumber}{rightNumber}");
        }
    }
}