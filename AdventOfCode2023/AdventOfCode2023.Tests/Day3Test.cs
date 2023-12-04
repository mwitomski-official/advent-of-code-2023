using AdventOfCode2023.App.Day3;

namespace AdventOfCode2023.Tests;

internal class Day3Test
{
    [SetUp]
    public void Setup()
    {
    }

    [Test] // 556057
    public void Solve_EngineSchematic_ReturnsCorrectValue()
    {
        int myScore = 556057;
        GearRatioManager gearRatioManager = new();
        var result = gearRatioManager.SolveEngineSchematic();

        Assert.That(result, Is.EqualTo(myScore));
    }

    [Test] //82824352
    public void Solve_ImprovedEngineSchematic_ReturnsCorrectValue()
    {
        int myScore = 82824352;
        GearRatioManager gearRatioManager = new();
        var result = gearRatioManager.SolveImprovedEngineSchematic();

        Assert.That(result, Is.EqualTo(myScore));
    }
}
