// See https://aka.ms/new-console-template for more information


using AdventOfCode2023.App.Day1;

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
