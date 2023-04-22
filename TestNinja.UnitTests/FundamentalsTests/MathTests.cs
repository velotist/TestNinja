using System.Linq;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.FundamentalsTests
{
    [TestFixture]
    public class MathTests
    {
        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        private Math _math;

        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [Ignore("For test purpose")]
        [TestCase(2, 1, 2)]
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

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbers()
        {
            const int limit = 10;
            var expected = new[]
            {
                1, 3, 5, 7, 9
            };

            var result = _math.GetOddNumbers(limit);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));
            Assert.That(result, Does.Contain(7));
            Assert.That(result, Does.Contain(9));

            // To sum up all of the above assertions we can assert like
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetOddNumbers_LimitIsLessThanZero_ReturnEmptyCollection()
        {
            const int limit = -10;
            var expected = new int[] { };

            var result = _math.GetOddNumbers(limit);

            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void GetOddNumbers_LimitIsZero_ReturnEmptyCollection()
        {
            const int limit = 0;
            var expected = new int[] { };

            var result = _math.GetOddNumbers(limit);

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}