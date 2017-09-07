using IGrains;
using Orleans;
using Orleans.Runtime.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_Implicit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client_Implicit";
            var config = ClientConfiguration.LoadFromFile("ClientConfiguration.xml");

            Console.ReadKey();

            while(true)
            {
                try
                {
                    GrainClient.Initialize(config);
                    Console.WriteLine("Connected to silo");
                    break;
                }catch(Exception ex)
                {
                    Console.WriteLine("Silo not available! Retrying in 3 seconds.");
                    Thread.Sleep(3000);
                }
            }
            
            ImplicitPublisher();

            Console.ReadLine();
        }

        static void ImplicitPublisher()
        {
            while(true)
            {
                Console.WriteLine("Press 'exit' to exit...");
                var input = Console.ReadLine();
                if (input == "exit") break;
                var publisherGrain = GrainClient.GrainFactory.GetGrain<IPublisherGrain>(Guid.Empty);
                publisherGrain.PublishMessageAsync(input);
            }
        }
    }
}
