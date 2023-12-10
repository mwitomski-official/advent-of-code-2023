using AdventOfCode2023.App.Day9;

namespace AdventOfCode2023.Tests;

public class Day9Test
{
    [Test] // 1992273652
    public void GetSumOfExtrapolateForwards_ReturnsCorrectValue()
    {
        int myScore = 1992273652;
        MirageMaintenanceManager mirageMaintenanceManager = new();
        var result = mirageMaintenanceManager.GetSumOfExtrapolateForwards();

        Assert.That(result, Is.EqualTo(myScore));
    }

    [Test] // 1012
    public void GetSumOfExtrapolateBackwards_ReturnsCorrectValue()
    {
        int myScore = 1012;
        MirageMaintenanceManager mirageMaintenanceManager = new();
        var result = mirageMaintenanceManager.GetSumOfExtrapolateBackwards();

        Assert.That(result, Is.EqualTo(myScore));
    }
}
