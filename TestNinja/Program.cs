using System;
using TestNinja.Mocking.VideoService;

namespace TestNinja
{
    public class Program
    {
        public static void Main()
        {
            var service = new VideoService();
            var title = service.ReadVideoTitle();

            Console.WriteLine(title);

            Console.ReadKey();
        }
    }
}