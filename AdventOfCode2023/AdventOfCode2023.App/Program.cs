// See https://aka.ms/new-console-template for more information


using AdventOfCode2023.App.Day1;
using AdventOfCode2023.App.Day2;
using AdventOfCode2023.App.Day3;
using AdventOfCode2023.App.Day4;

// Day 1 - 01.12.2023
// Task 1.1
RecoveryManager recoveryManager = new();
int recoveryManagerResponse = recoveryManager
    .RunCalibration();
Console.WriteLine($"Task 1.1 - Your puzzle answer was: {recoveryManagerResponse}");

// Task 1.2
int secondRecoveryManagerResponse = recoveryManager
    .RunSecondCalibration();
Console.WriteLine($"Task 1.2 - Your puzzle answer was: {secondRecoveryManagerResponse}");

// Day 2 - 02.12.2023
// Task 2.1
// 12 red cubes, 13 green cubes, and 14 blue cubes
CubeManager cubeManager = new();
int sumOfAvailableGameIds = cubeManager.GetSumOfAvailableGameIds(new CubeBag
{
    RedCubes = 12,
    BlueCubes = 13,
    GreenCubes = 14,
});

Console.WriteLine($"Task 2.1 - Your puzzle answer was: {sumOfAvailableGameIds}");

// Task 2.2

int result = cubeManager.GetCalcMaxNumberOfCubesEachColor();

Console.WriteLine($"Task 2.2 - Your puzzle answer was: {result}");


// Day 3 - 03.12.2023
// Task 3.1
GearRatioManager gearRatioManager = new();
var solvedEngineSchematic = gearRatioManager.SolveEngineSchematic();
Console.WriteLine($"Task 3.1 - Your puzzle answer was: {solvedEngineSchematic}");


var solvedImprovedEngineSchematic = gearRatioManager.SolveImprovedEngineSchematic();
Console.WriteLine($"Task 3.2 - Your puzzle answer was: {solvedImprovedEngineSchematic}");


// Day 4 - 04.12.2023
// Task 4.1
ScratchcardsManager scratchcardsManager = new();
var scratchcardsTotalPoints = scratchcardsManager.GetTotalPoints();
Console.WriteLine($"Task 3.1 - Your puzzle answer was: {scratchcardsTotalPoints}");

// Task 4.2
var scratchcardsTotalPoints2 = scratchcardsManager.GetTotalPointsincludedCopyOfCards();
Console.WriteLine($"Task 3.1 - Your puzzle answer was: {scratchcardsTotalPoints2}");