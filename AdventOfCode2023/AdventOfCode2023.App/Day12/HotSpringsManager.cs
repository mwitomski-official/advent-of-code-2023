using AdventOfCode2023.App.Services;

namespace AdventOfCode2023.App.Day12;

public class HotSpringsManager
{
    // Transforms the integer array by subtracting 1 from the first element.
    private int[] Transform(int[] t) => new int[] { t[0] - 1 }.Concat(t.Skip(1)).ToArray();

    // Retrieves the sum of arrangements for the given data.
    public int GetSumOfCounts()
    {
        var data = Parse("Day12\\Data.txt");
        
        // Calculate and print the sum of arrangements for the original data.
        return data.Sum(tuple => ArrangementCount(tuple.Item1, tuple.Item2, false));
    }

    // Parses the input file and returns a list of tuples containing strings and arrays of integers.
    private List<(string, int[])> Parse(string filename)
    {
        var data = new List<(string, int[])>();
        foreach (var line in File.ReadLines(filename))
        {
            var parts = line.Split(" ");
            var a = parts[0];
            var b = parts[1].Split(",").Select(int.Parse).ToArray();
            data.Add((a, b));
        }
        return data;
    }

    // Recursive function to count the arrangements based on the given conditions.
    private int ArrangementCount(string m, int[] s, bool n)
    {
        if (!s.Any())
        {
            return m.Contains("#") ? 0 : 1;
        }
        else if (string.IsNullOrEmpty(m))
        {
            return s.Sum() == 0 ? 1 : 0;
        }
        else if (s[0] == 0)
        {
            return m[0] == '?' || m[0] == '.' ? ArrangementCount(m.Substring(1), s.Skip(1).ToArray(), false) : 0;
        }
        else if (n)
        {
            return m[0] == '?' || m[0] == '#' ? ArrangementCount(m.Substring(1), Transform(s), true) : 0;
        }
        else if (m[0] == '#')
        {
            return ArrangementCount(m.Substring(1), Transform(s), true);
        }
        else if (m[0] == '.')
        {
            return ArrangementCount(m.Substring(1), s, false);
        }
        else
        {
            return ArrangementCount(m.Substring(1), s, false) + ArrangementCount(m.Substring(1), Transform(s), true);
        }
    }

}
