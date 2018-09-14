using System;

namespace Application.Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application is starting");
            WebhostInitiator.RunWebHost(args);
            Console.WriteLine("Application is starting");
        }
        
    }
}
