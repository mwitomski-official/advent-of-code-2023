using AdventOfCode2023.App.Extensions;
using AdventOfCode2023.App.Services;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Day1;

public partial class RecoveryManager
{
    [GeneratedRegex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]")]
    private static partial Regex NumberRegex();
    [GeneratedRegex("(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)|[0-9]", RegexOptions.RightToLeft)]
    private static partial Regex NumberReversedRegex();
    public string[] Lines { get; set; }

    public RecoveryManager()
    {
      Lines ??= FileManger.Read("Day1\\Data.txt");
    }

    /// <summary>
    /// The first and last digits from the string form a number, based on which the total for each line will be counted
    /// </summary>
    /// <returns></returns>
    public int RunCalibration()
        => Lines
           .Select(l => string.Join("", l.Where(g => char.IsDigit(g))))
           .Sum(l => int.Parse(l.FirstOrDefault().ToString() + l.LastOrDefault().ToString()));

    /// <summary>
    /// The first and last digits from the string form a number, 
    /// based on which the total for each line will be counted. 
    /// Includes figures written in words
    /// </summary>
    /// <returns></returns>
    public int RunSecondCalibration()
        => Lines
        .Select(line => GetNumber(line))
        .Sum();

    /// <summary>
    /// Returns the number that consists of the first and last digits found in the string, 
    /// taking into account the digits written in words
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private int GetNumber(string line)
    {
        var numberRegex = NumberRegex();
        var numberRegexFromRight = NumberReversedRegex();

        // Set [left number] using [Number Regex]
        string leftNumber = numberRegex
            .Match(line).Value;

        // Set [right number] using [Number Regex From Right]
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