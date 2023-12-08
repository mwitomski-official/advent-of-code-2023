using AdventOfCode2023.App.Services;
using System;

namespace AdventOfCode2023.App.Day8;

public class ZZZManager
{
    private string[] Lines { get; set; } = [];

    private static readonly char[] separator = [' ', ',', '(', ')', '='];

    public ZZZManager()
    {
        Lines = FileManger.Read("Day8\\Data.txt");
    }

    // 15517
    public int GetStepsRequiredToReachZZZ()
    {
        var instructions = Lines[0]
            .Select(x => x == 'L' ? 0 : 1)
            .ToArray();

        var nodes = Lines
            .Skip(2)
            .Select(x => x.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(x => x[0], x => x[1..]);

        long result = 0;

        // Start from the initial node 'AAA'
        var currentNode = "AAA";

        // Loop until the current node reaches 'ZZZ'
        while (currentNode != "ZZZ")
        {
            // Get the current instruction based on the result1 index
            // `result % instructions.Length:`
            // This calculates the remainder when result is divided by the length of the instructions array.
            // It's used to loop back to the beginning of the instructions array when the end is reached.

            // `instructions[result % instructions.Length]:`
            // This expression retrieves the instruction at the calculated index, effectively cycling through the instructions array.
            var currentInstruction = instructions[result % instructions.Length];

            // Get the array of neighbors for the current node
            var neighbors = nodes[currentNode];

            // Move to the next node based on the current instruction
            var nextNode = neighbors[currentInstruction];

            // Update the currentNode for the next iteration
            currentNode = nextNode;

            // Increment the result counter
            result++;
        }

        // The number of steps required to reach 'ZZZ' is returned
        return (int)result;
    }
}
