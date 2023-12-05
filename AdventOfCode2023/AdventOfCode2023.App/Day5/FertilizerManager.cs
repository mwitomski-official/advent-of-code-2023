using AdventOfCode2023.App.Services;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Day5
{
    record MapRow(long Source, long Destination, long Length);

    public class FertilizerManager
    {
        private string[] Lines { get; set; }
        private List<List<MapRow>> Maps { get; set; }

        public FertilizerManager()
        {
            Lines ??= FileManger.Read("Day5\\Data.txt");
            Maps = [];
        }

        // ------ MAIN --------

        public string GetLowestLocationNumber()
        {
            var wipMap = new List<MapRow>();
            var seeds = GetSeeds();
            var clearLines = GetClearLines();

            // TODO: Improve this engine
            foreach (var line in clearLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (wipMap.Count > 0)
                        Maps.Add(wipMap);
                    wipMap = [];
                    continue;
                }

                var parts = line.Split(' ').Select(s => long.Parse(s)).ToArray();
                wipMap.Add(new MapRow(parts[1], parts[0], parts[2]));
            }

            if (wipMap.Count > 0)
                Maps.Add(wipMap);

            var lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                var location = ResolveAll(seed, Maps);
                if (location < lowestLocation)
                    lowestLocation = location;
            }

            var result = lowestLocation.ToString();
            return result;
        }

        // ResolveAll method: Resolve the initial value for each seed across all maps.
        private long ResolveAll(long value, List<List<MapRow>> maps)
        {
            // Iterate through each map in the list and resolve the value using the Resolve method for each map.
            foreach (var map in maps)
            {
                value = Resolve(value, map);
            }

            // Return the final resolved value.
            return value;
        }

        // Resolve method: Resolve the initial value within a specific map.
        private static long Resolve(long value, List<MapRow> map)
        {
            // Iterate through each MapRow in the map.
            foreach (var row in map)
            {
                // Calculate an offset by subtracting the Source value from the value.
                var offset = value - row.Source;

                // Check if the offset is within the range of the MapRow (between 0 and Length).
                if (offset >= 0 && offset < row.Length)
                    // Return the Destination value plus the offset.
                    return row.Destination + offset;
            }

            // Return the initial value if no match is found in any MapRow.
            return value;
        }

        // ------ HELPER --------

        private List<long> GetSeeds()
            => ExtractNumbers(Lines[0]);

        private List<string> GetClearLines()
        {
            // Define the regular expression pattern
            string pattern = @"\bmap\b:";
            var clearLines = Lines
                // Skip Seeds
                .Skip(1)
                // Remove lines that match the specified regular expression pattern
                .Where(line => !Regex.IsMatch(line, pattern))
                .ToList();
            return clearLines;
        }

        // Parse seed values from the first line and continue to the next line if there are no seed values.
        private List<long> ExtractNumbers(string input)
        {
            // Split the input string using whitespace
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Skip the first element ("seeds:") and parse the remaining elements as long
            return parts.Skip(1).Select(long.Parse).ToList();
        }
    }
}
