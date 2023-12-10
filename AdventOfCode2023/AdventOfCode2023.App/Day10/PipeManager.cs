using AdventOfCode2023.App.Services;

namespace AdventOfCode2023.App.Day10;

public record Pipe(int AX, int AY, int BX, int BY);

public class PipeManager
{
    private readonly string[] _lines; 

    // Define the possible next positions for each pipe type
    private static readonly Dictionary<char, HashSet<(int, int)>> NextPipe = new()
    {
        {'|', new HashSet<(int, int)>{(0, 1), (0, -1)}}, // down, up
        {'-', new HashSet<(int, int)>{(1, 0), (-1, 0)}}, // right, left
        {'L', new HashSet<(int, int)>{(0, -1), (1, 0)}}, // up, right
        {'J', new HashSet<(int, int)>{(0, -1), (-1, 0)}}, // up, left
        {'7', new HashSet<(int, int)>{(-1, 0), (0, 1)}}, // left, down
        {'F', new HashSet<(int, int)>{(0, 1), (1, 0)}} // down, right
    };

    public PipeManager()
    {
        _lines ??= FileManger.Read("Day10\\Data.txt");
    }

    public (int, int) Execute()
    {
        // Read the input file and initialize the grid
        var grid = _lines.Select(line => line.Trim().ToCharArray()).ToList();
        var dist = new Dictionary<(int, int), int>();
        (int x, int y) start = (0, 0);

        // Find the starting position
        foreach (var (row, y) in grid.Select((row, y) => (row, y)))
        {
            if (row.Contains('S'))
            {
                start = (Array.IndexOf(row, 'S'), y);
                break;
            }
        }

        // Set the starting point as 'F'
        grid[start.y][start.x] = 'F';

        // Calculate and print the result for distance / 2
        int distance = GetDistance(grid, dist, start);

        // Calculate and print the result for tileCount
        int tileCount = GetTileCount(grid, dist);

        return (distance, tileCount);
    }

    // Method to calculate the distance / 2
    private static int GetDistance(List<char[]> grid, Dictionary<(int, int), int> dist, (int x, int y) start)
    {
        int distance = 0;
        var currentPos = start;

        // Loop until the current position is visited
        while (!dist.ContainsKey(currentPos))
        {
            dist[currentPos] = distance;
            distance++;

            var (x, y) = currentPos;
            var currentPipe = grid[y][x];

            // Check possible next positions and update current position
            foreach (var (dx, dy) in NextPipe[currentPipe])
            {
                int nx = x + dx;
                int ny = y + dy;

                if (!dist.ContainsKey((nx, ny)))
                {
                    currentPos = (nx, ny);
                    break;
                }
            }
        }

        return distance / 2;
    }

    // Method to calculate the tileCount
    private static int GetTileCount(List<char[]> grid, Dictionary<(int, int), int> dist)
    {
        int tileCount = 0;

        foreach (var (row, y) in grid.Select((row, y) => (row, y)))
        {
            int parity = 0;
            foreach (var (c, x) in row.Select((c, x) => (c, x)))
            {
                if (!dist.ContainsKey((x, y))) // ground or junk pipe
                {
                    if (parity % 2 == 1)
                    {
                        tileCount++;
                    }
                    continue;
                }

                if (new[] { '|', 'L', 'J' }.Contains(c)) // L---J and F----7 do not increase parity
                {
                    parity++;
                }
            }
        }

        return tileCount;
    }
}
