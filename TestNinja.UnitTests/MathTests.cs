using System.Security.Policy;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2,1,2)]
        [TestCase(5, 2, 5)]
        [TestCase(1, 2, 2)]
        [TestCase(3, 5, 5)]
        public void Max_WhenCalled_ReturnGreaterArgument(int firstNumber, int secondNumber, int expected)
        {
            var result = _math.Max(firstNumber, secondNumber);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(2, 2, 2)]
        [TestCase(9, 9, 9)]
        public void Max_AreBothArguments_ReturnSameArgument(int firstNumber, int secondNumber, int expected)
        {
            var result = _math.Max(firstNumber, secondNumber);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
