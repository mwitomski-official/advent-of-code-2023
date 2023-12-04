using AdventOfCode2023.App.Services;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Day4;

public class ScratchcardsManager
{
    private string[] Lines { get; set; }
    private string[] TestLines { get; set; }

    public ScratchcardsManager()
    {
        Lines ??= FileManger.Read("Day4\\Data.txt");
        TestLines ??= FileManger.Read("Day4\\TestData.txt");
    }

    // ------ MAIN --------
    // 25571
    public int GetTotalPoints()
    {
        // Extract the winning and participant numbers from each line
        var scratchcardsData = Lines.Select(line => line.Split(':')[1].Trim().Split(" | "));

        // Calculate the total points based on the count of intersecting numbers
        return CalculateTotalPoints(scratchcardsData);
    }

    // ------ HELPER --------
    // Helper method to calculate the total points based on intersecting numbers
    int CalculateTotalPoints(IEnumerable<string[]> scratchcardsData)
    {
        // Calculate the count of intersecting numbers for each scratchcard
        var intersectingNumbersCounts = CalculateIntersectingNumbersCounts(scratchcardsData);

        // Calculate the total points based on the count of intersecting numbers
        return intersectingNumbersCounts.Sum(value => (int)Math.Pow(2, value - 1));
    }

    // Helper method to calculate the count of intersecting numbers for each scratchcard
    IEnumerable<int> CalculateIntersectingNumbersCounts(IEnumerable<string[]> scratchcardsData)
    {
        return scratchcardsData.Select(pair =>
        {
            // Split and trim the numbers on both sides
            var winningNumbers = pair[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var participantNumbers = pair[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // Count the intersecting numbers
            return winningNumbers.Intersect(participantNumbers).Count();
        });
    }

    // ------ LAB ------
    // 2nd part doesn't work
    public int GetTotalPointsIncludedCopyOfCards()
    {
        // Extract scratchcards data from input lines
        var scratchcards = ExtractScratchcardsData(Lines);

        var totalScratchcards = 0;

        // Iterate through scratchcards to calculate total points and update counts
        for (int currentIndex = 0; currentIndex < scratchcards.Length; currentIndex++)
        {
            var (winningNumbers, count) = scratchcards[currentIndex];

            // Update counts for next levels of copies
            UpdateCountsForNextLevels(scratchcards, currentIndex, winningNumbers, count);

            // Accumulate total scratchcards
            totalScratchcards += count;
        }

        return totalScratchcards;
    }

    // Extract scratchcards data from input lines
    private (int winningNumbers, int count)[] ExtractScratchcardsData(string[] lines)
    {
        return lines
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList())
            .Select(numbers =>
            {
                // Extract winning and participant numbers for each scratchcard
                var winningNumbers = numbers.TakeWhile(x => x != "|").ToHashSet();
                var participantNumbers = numbers.SkipWhile(x => x != "|").Skip(1).ToHashSet();
                // Calculate the count of matching numbers and set initial count to 1
                return (winningNumbers: winningNumbers.Intersect(participantNumbers).Count(), count: 1);
            })
            .ToArray();
    }

    // Update counts for next levels of copies
    private void UpdateCountsForNextLevels((int winningNumbers, int count)[] scratchcards, int currentIndex, int winningNumbers, int count)
    {
        for (int nextIndex = currentIndex + 1, remainingLevels = winningNumbers; nextIndex < scratchcards.Length && remainingLevels > 0; nextIndex++, remainingLevels--)
        {
            // Increment the count for next levels of copies
            scratchcards[nextIndex].count += count;
        }
    }
}
