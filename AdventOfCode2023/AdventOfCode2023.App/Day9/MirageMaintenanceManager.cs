using AdventOfCode2023.App.Services;

namespace AdventOfCode2023.App.Day9;

public class MirageMaintenanceManager
{
    private string[] Lines { get; set; }
    private IEnumerable<List<int[]>> Histories { get; set; }

    public MirageMaintenanceManager()
    {
        Lines ??= FileManger.Read("Day9\\Data.txt");
        Histories = Lines.Select(FormHistory);
    }

    // 1992273652
    // ------ MAIN --------
    // Get the sum of extrapolation forwards for all histories
    public int GetSumOfExtrapolateForwards()
        => Histories.Sum(ExtrapolateForwards);

    // Get the sum of extrapolation backwards for all histories
    public int GetSumOfExtrapolateBackwards()
        => Histories.Sum(ExtrapolateBackwards);

    // ------ HELPER --------

    // IMPORTANT 

    // The sequences[^1] syntax is using the C# 8.0 feature called "Indexers" and "Range" expressions.
    // In this case, sequences[^1] is equivalent to sequences[sequences.Count - 1],
    // and it retrieves the last element in the sequences list.
    // The ^1 index represents an index relative to the end of the sequence, and ^1 refers to the last element.

    // Here's a breakdown:

    // - ^1: This is a shorthand syntax for indexing elements from the end of the sequence.
    // ^1 means the last element, ^2 means the second-to-last element, and so on.
    // - sequences[^1]: This accesses the last element of the sequences list.

    // Method to form a history from a report string
    private static List<int[]> FormHistory(string report)
    {
        // Split the report into initial values and convert them to an array of integers
        var initial = report
            .Split(" ")
            .Select(s => int.Parse(s))
            .ToArray();

        // Create a list to store sequences, starting with the initial values
        var sequences = new List<int[]> { initial };

        // Continue forming sequences until the last sequence contains only zeros
        while (sequences[^1].Any(v => v != 0))
        {
            // Add a new sequence formed by subtracting each element from the previous sequence
            sequences.Add(item: sequences[^1]
                .Skip(1)
                .Select((val, i) => val - sequences[^1][i])
                .ToArray());
        }

        // Return the list of sequences forming the history
        return sequences;
    }

    // Method to extrapolate forwards based on sequences in a history
    private static int ExtrapolateForwards(IList<int[]> sequences)
    {
        // Sum the [last elements] of each sequence in reverse order (excluding the last sequence)
        return sequences
            .Reverse()
            .Skip(1)
            .Aggregate(seed: 0, func: (n, seq) => n + seq[^1]);
    }

    // Method to extrapolate backwards based on sequences in a history
    private static int ExtrapolateBackwards(IList<int[]> sequences)
    {
        // Sum the [first elements] of each sequence in reverse order (excluding the last sequence)
        return sequences
            .Reverse()
            .Skip(1)
            .Aggregate(seed: 0, func: (n, seq) => seq[0] - n);
    }
}
