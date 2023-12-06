using AdventOfCode2023.App.Services;

namespace AdventOfCode2023.App.Day6
{
    record BoatInfo(int Time, long RecordDistance);

    public class BoatManager
    {
        private string[] Lines { get; set; }

        // _distanceRecord Function:
        // This function checks whether the boat can beat the record given a BoatInfo and a hold time.
        // It uses the condition (boatInfo.Time - hold) * hold > boatInfo.RecordDistance.

        // Function to check if the boat can beat the record given the hold time
        private Func<BoatInfo, long, bool> _distanceRecord = (boatInfo, hold)
            => (boatInfo.Time - hold) * hold > boatInfo.RecordDistance;

        public BoatManager()
        {
            Lines ??= FileManger.Read("Day6\\Data.txt");
        }

        public int GetNumberOfWaysYouCanBeatTheRecord()
        {
            // Extract race times from the first line of Lines
            var raceTimes = Lines.FirstOrDefault()?
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
                .ToArray();

            // Extract record distances from the last line of Lines
            var recordDistances = Lines.LastOrDefault()?
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
                .ToArray();

            // Create a list of BoatInfo objects
            List<BoatInfo> boatInfos = Enumerable.Range(0, raceTimes.Length)
                .Select(i => new BoatInfo(raceTimes[i], recordDistances[i]))
                .ToList();

            // Initialize the result to 1
            int result = boatInfos
                // Take the first 4 BoatInfo objects
                .Take(4)
                // Calculate the count of ways to beat the record for each BoatInfo
                .Select(boatInfo =>
                    Enumerable.Range(0, boatInfo.Time + 1)
                    .Count(hold => _distanceRecord.Invoke(boatInfo, hold)))
                // Multiply the counts together using Aggregate
                .Aggregate(1, (acc, count) => acc * count);

            return result;
        }

        public int GetNumberOfWaysYouCanBeatTheRecordSingle()
        {
            // Extract race times from the first line of Lines
            var raceTimes = int.Parse(string.Join("", Lines[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)));

            // Extract record distances from the last line of Lines
            var recordDistances = long.Parse(string.Join("", Lines[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)));

            // Create a list of BoatInfo objects
            List<BoatInfo> boatInfos = new();
            boatInfos.Add(new BoatInfo(raceTimes, recordDistances));

            var result = Enumerable
                .Range(0, boatInfos[0].Time + 1)
                .Count(x => _distanceRecord.Invoke(boatInfos[0], x));

            return result;
        }
    }
}
