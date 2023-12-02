using AdventOfCode2023.App.Services;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Day2;

public class CubeManager
{
    private string[] Lines { get; set; }
    public CubeManager()
    {
        Lines ??= FileManger.Read("Day2\\Data.txt");
    }

    // ------ MAIN --------
    public int GetSumOfAvailableGameIds(CubeBag elfBag)
    {
        if (elfBag == null) return 0;
        List<CubeGames> allGames = GetAllGames();
        return allGames
            .Where(game =>
                !game.RedCubes.Any(red => red > elfBag.RedCubes) &&
                !game.GreenCubes.Any(green => green > elfBag.GreenCubes) &&
                !game.BlueCubes.Any(blue => blue > elfBag.BlueCubes))
            .Select(game => game.Id)
            .ToList()
            .Sum();
    }

    public int GetCalcMaxNumberOfCubesEachColor()
    {
        List<CubeGames> allGames = GetAllGames();
        return allGames.Sum(item =>
        {
            var redMax = item.RedCubes.Max();
            var greenMax = item.GreenCubes.Max();
            var blueMax = item.BlueCubes.Max();

            return redMax * greenMax * blueMax;
        });
    }

    // ------ HELPER --------
    private List<CubeGames> GetAllGames()
        => Lines.Select(game =>
        {
            var gameId = game.Split(':')[0].Split(" ")[1];
            return new CubeGames
            {
                Id = int.Parse(gameId),
                RedCubes = ExtractColorCount(game, "red"),
                GreenCubes = ExtractColorCount(game, "blue"),
                BlueCubes = ExtractColorCount(game, "green")
            };
        }).ToList();

    private int[] ExtractColorCount(string input, string color)
    {
        string pattern = $@"(\d+) {color}";
        Regex regex = new(pattern);

        MatchCollection matches = regex.Matches(input);

        List<int> countList = [];
        foreach (Match match in matches)
        {
            if (match.Groups.Count == 2)
            {
                int.TryParse(match.Groups[1].Value, out int colorCount);
                countList.Add(colorCount);
            }
        }

        return [.. countList];
    }
}

public class CubeGames
{
    public int Id { get; set; }
    public int[] RedCubes { get; set; } = [];
    public int[] GreenCubes { get; set; } = [];
    public int[] BlueCubes { get; set; } = [];
}

public class CubeBag
{
    public int RedCubes { get; set; }
    public int GreenCubes { get; set; }
    public int BlueCubes { get; set; }
}
