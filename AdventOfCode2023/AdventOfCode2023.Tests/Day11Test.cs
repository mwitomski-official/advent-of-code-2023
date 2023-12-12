using AdventOfCode2023.App.Day11;

namespace AdventOfCode2023.Tests;

public class Day11Test
{
    [Test] // 9608724
    public void GetSumOfLengtOfShortesPathBetweenEveryPairOfGalaxies_ReturnsCorrectValue()
    {
        int myScore = 9608724;
        CosmicExpansionManager sumOfLengtOfShortesPathBetweenEveryPairOfGalaxies = new();
        var result = sumOfLengtOfShortesPathBetweenEveryPairOfGalaxies
            .GetSumOfLengtOfShortesPathBetweenEveryPairOfGalaxies();

        Assert.That(result, Is.EqualTo(myScore));
    }
}
