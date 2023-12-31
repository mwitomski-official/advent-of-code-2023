﻿using AdventOfCode2023.App.Day3;
using AdventOfCode2023.App.Day4;

namespace AdventOfCode2023.Tests;

public class Day4Test
{
    [SetUp]
    public void Setup()
    {
    }

    [Test] // 25571
    public void GetTotalPoints_ReturnsCorrectValue()
    {
        int myScore = 25571;
        ScratchcardsManager scratchcardsManager = new();
        var result = scratchcardsManager.GetTotalPoints();

        Assert.That(result, Is.EqualTo(myScore));
    }

    [Test] // 8805731
    public void GetTotalPointsIncludedCopyOfCards_ReturnsCorrectValue()
    {
        int myScore = 8805731;
        ScratchcardsManager scratchcardsManager = new();
        var result = scratchcardsManager.GetTotalPointsIncludedCopyOfCards();

        Assert.That(result, Is.EqualTo(myScore));
    }
}
