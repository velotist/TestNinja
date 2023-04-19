using System;
using TestNinja.Mocking;

namespace TestNinja
{
    public class Program
    {
        public static void Main()
        {
            var service = new VideoService();
            var title = service.ReadVideoTitle(new FileReader());

            Console.WriteLine(title);

            Console.ReadKey();
        }
    }
}
