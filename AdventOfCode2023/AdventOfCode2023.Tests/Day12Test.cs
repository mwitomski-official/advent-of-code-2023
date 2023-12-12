using AdventOfCode2023.App.Day12;

namespace AdventOfCode2023.Tests;

public class Day12Test
{
    [Test] // 7236
    public void GetSumOfCounts_ReturnsCorrectValue()
    {
        int myScore = 7236;
        HotSpringsManager sumOfArrangements = new();
        var result = sumOfArrangements
            .GetSumOfCounts();

        Assert.That(result, Is.EqualTo(myScore));
    }
}
