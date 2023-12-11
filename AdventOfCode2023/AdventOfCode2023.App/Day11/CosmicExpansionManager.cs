using AdventOfCode2023.App.Services;

namespace AdventOfCode2023.App.Day11;

public class CosmicExpansionManager
{
    private readonly string[] _lines;

    public CosmicExpansionManager()
    {
        _lines ??= FileManger.Read("Day11\\Data.txt");        
    }

    public long GetSumOfLengtOfShortesPathBetweenEveryPairOfGalaxies()
    {
        // Load the universe data and expand rows with no galaxies
        var universe = new List<List<char>>();
        foreach (var line in _lines)
        {
            var row = line.Trim().ToList();
            universe.Add(row);
            if (!row.Contains('#'))
            {
                universe.Add(row.ToList()); // Make sure it is a copy
            }
        }

        // Find columns with no galaxies
        var emptyColumns = new List<int>();
        for (int x = 0; x < universe[0].Count; x++)
        {
            bool empty = true;
            for (int y = 0; y < universe.Count; y++)
            {
                if (universe[y][x] == '#')
                {
                    empty = false;
                    break;
                }
            }

            if (empty)
            {
                emptyColumns.Add(x);
            }
        }

        // Expand columns with no galaxies
        for (int y = 0; y < universe.Count; y++)
        {
            foreach (var (i, x) in emptyColumns.Select((x, i) => (i, x)))
            {
                universe[y].Insert(i + x, '.');
            }
        }

        // Find positions of galaxies in the expanded universe
        var galaxies = new List<(int, int)>();
        for (int y = 0; y < universe.Count; y++)
        {
            for (int x = 0; x < universe[0].Count; x++)
            {
                if (universe[y][x] == '#')
                {
                    galaxies.Add((x, y));
                }
            }
        }

        // Find distances between all pairs of galaxies
        long sumOfLengths = 0;
        foreach (var a in galaxies)
        {
            foreach (var b in galaxies)
            {
                sumOfLengths += Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
            }
        }

        // Divide by 2 because we have added distances from 'a' to 'b' and from 'b' to 'a'
        return sumOfLengths / 2;
    }
}
