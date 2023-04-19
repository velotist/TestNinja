using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockingTests
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}
