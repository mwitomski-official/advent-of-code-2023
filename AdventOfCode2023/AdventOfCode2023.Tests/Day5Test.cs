using AdventOfCode2023.App.Day5;

namespace AdventOfCode2023.Tests
{
    public class Day5Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test] // 993500720
        // What is the lowest location number that corresponds to any of the initial seed numbers?
        public void GetLowestLocationNumber_ReturnsCorrectValue()
        {
            string myScore = "993500720";
            FertilizerManager scratchcardsManager = new();
            var result = scratchcardsManager.GetLowestLocationNumber();

            Assert.That(result, Is.EqualTo(myScore));
        }
    }
}
