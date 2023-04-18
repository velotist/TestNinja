using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public  class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseStringWithTheElementStrong()
        {
            var formatter = new HtmlFormatter();
            const string startText = "<strong>";
            const string text = "This is a test.";
            const string endText = "</strong>";
            var expected = $"{startText}{text}{endText}";

            var result = formatter.FormatAsBold(text);

            Assert.That(result, Is.EqualTo(expected).IgnoreCase);

            // More general would be like
            Assert.That(result, Does.StartWith(startText).IgnoreCase);
            Assert.That(result, Does.EndWith(endText).IgnoreCase);
            Assert.That(result, Does.Contain(text).IgnoreCase);
        }
    }
}
