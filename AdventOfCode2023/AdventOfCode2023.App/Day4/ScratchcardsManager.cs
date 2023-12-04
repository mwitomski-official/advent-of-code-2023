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
    public int GetTotalPointsincludedCopyOfCards()
    {
        var scratchcardsData = TestLines.Select(line => line.Split(':')[1].Trim().Split(" | "));
        var scratchcardsCount = CalculateTotalScratchcards(scratchcardsData);

        return scratchcardsCount;
    }

    int CalculateTotalScratchcards(IEnumerable<string[]> scratchcardsData)
    {
        var scratchcardsDictionary = new Dictionary<int, int>();

        foreach (var pair in scratchcardsData)
        {
            var winningNumbers = pair[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var participantNumbers = pair[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int matchingNumbersCount = winningNumbers.Intersect(participantNumbers).Count();

            // Calculate the count of copies for the current card
            int copiesCount = (int)Math.Pow(2, matchingNumbersCount - 1);

            // Update the count of scratchcards in the dictionary
            if (!scratchcardsDictionary.ContainsKey(copiesCount))
            {
                scratchcardsDictionary[copiesCount] = 1;
            }
            else
            {
                scratchcardsDictionary[copiesCount]++;
            }

            // Process copies for the current card
            ProcessCopies(scratchcardsDictionary, copiesCount);
        }

        // Calculate the total count of scratchcards
        return scratchcardsDictionary.Values.Sum();
    }

    void ProcessCopies(Dictionary<int, int> scratchcardsDictionary, int copiesCount)
    {
        // Iterate through copies and process further copies if needed
        var existingKeys = scratchcardsDictionary.Keys.ToList(); // To avoid modifying the dictionary during iteration

        foreach (var key in existingKeys)
        {
            // Check if the next level of copies exists in the dictionary
            var nextLevelKey = key * 2;

            if (!scratchcardsDictionary.ContainsKey(nextLevelKey))
            {
                scratchcardsDictionary[nextLevelKey] = 0;  // Initialize the count if it doesn't exist
            }

            // Update the count of scratchcards for the next level of copies
            scratchcardsDictionary[nextLevelKey] += scratchcardsDictionary[key];
        }
    }
}
