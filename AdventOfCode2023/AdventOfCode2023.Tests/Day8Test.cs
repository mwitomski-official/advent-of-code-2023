using AdventOfCode2023.App.Day8;

namespace AdventOfCode2023.Tests;

public class Day8Test
{
    [Test] // 15517
    public void GameWithJokers_ReturnsCorrectValue()
    {
        int myScore = 15517;
        ZZZManager zzzManager = new();
        var result = zzzManager.GetStepsRequiredToReachZZZ();

        Assert.That(result, Is.EqualTo(myScore));
    }
}
