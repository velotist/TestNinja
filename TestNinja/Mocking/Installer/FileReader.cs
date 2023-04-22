using System.IO;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking.Installer
{
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}