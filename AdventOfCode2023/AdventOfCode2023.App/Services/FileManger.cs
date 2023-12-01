namespace AdventOfCode2023.App.Services
{
    public static class FileManger
    {
        public static string[] Read(string path) 
            => File.ReadLines(path).ToArray();
    }
}
