using AdventOfCode2023.App.Day2;

namespace AdventOfCode2023.Tests
{
    public class Day2Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Sum_OfGameIds_Correct()
        {
            int myScore = 2685;
            CubeManager cubeManager = new();
            CubeBag elfBag = new()
            {
                RedCubes = 12,
                BlueCubes = 13,
                GreenCubes = 14,
            };

            var result = cubeManager.GetSumOfAvailableGameIds(elfBag);

            Assert.That(result, Is.EqualTo(myScore));
        }
        
        [Test]
        public void Get_CalcMaxNumberOfCubesEachColor_Correct()
        {
            int myScore = 83707;
            CubeManager cubeManager = new();

            var result = cubeManager.GetCalcMaxNumberOfCubesEachColor();

            Assert.That(result, Is.EqualTo(myScore));
        }
    }
}