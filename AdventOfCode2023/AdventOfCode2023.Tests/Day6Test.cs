using AdventOfCode2023.App.Day6;

namespace AdventOfCode2023.Tests
{
    public class Day6Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test] // 6209190
        public void GetNumberOfWaysYouCanBeatTheRecord_ReturnsCorrectValue()
        {
            int myScore = 6209190;
            BoatManager boatManager = new();
            var result = boatManager.GetNumberOfWaysYouCanBeatTheRecord();

            Assert.That(result, Is.EqualTo(myScore));
        }

        [Test] // 28545089
        public void GetNumberOfWaysYouCanBeatTheRecordSingle_ReturnsCorrectValue()
        {
            int myScore = 28545089;
            BoatManager boatManager = new();
            var result = boatManager.GetNumberOfWaysYouCanBeatTheRecordSingle();

            Assert.That(result, Is.EqualTo(myScore));
        }
    }
}
