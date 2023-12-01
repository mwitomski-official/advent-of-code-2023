namespace AdventOfCode2023.App.Extensions
{
    public static class NewExtensions
    {
        public static string ToDigit(this string str)
        {
            var dict = new Dictionary<string, string>
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };

            return dict[str];
        }
    }
}
