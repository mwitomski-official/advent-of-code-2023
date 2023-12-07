using AdventOfCode2023.App.Day6;
using AdventOfCode2023.App.Day7;

namespace AdventOfCode2023.Tests
{
    public class Day7Test
    {
        [Test] // 253205868
        public void GameWithJokers_ReturnsCorrectValue()
        {
            int myScore = 253205868;
            CardManager cardManager = new();
            var result = cardManager.ParseGames();

            Assert.That(result, Is.EqualTo(myScore));
        }

        [Test] // 253907829
        public void GameWithoutJokers_ReturnsCorrectValue()
        {
            int myScore = 253907829;
            CardManager cardManager = new();
            var result = cardManager.ParseGames(jokers: true);

            Assert.That(result, Is.EqualTo(myScore));
        }
    }
}
