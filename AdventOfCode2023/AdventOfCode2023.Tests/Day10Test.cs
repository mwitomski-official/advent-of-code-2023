using AdventOfCode2023.App.Day10;

namespace AdventOfCode2023.Tests;

public class Day10Test
{
    [Test] // 6773
    public void GetDistance_ReturnsCorrectValue()
    {
        int myScore = 6773;
        PipeManager pipeManager = new();
        var result = pipeManager.Execute();

        Assert.That(result.Item1, Is.EqualTo(myScore));
    }

    [Test] // 493
    public void GetTileCount_ReturnsCorrectValue()
    {
        int myScore = 493;
        PipeManager pipeManager = new();
        var result = pipeManager.Execute();

        Assert.That(result.Item2, Is.EqualTo(myScore));
    }
}
