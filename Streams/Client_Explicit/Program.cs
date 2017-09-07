using IGrains;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_Explicit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client_Explicit";
            var config = ClientConfiguration.LoadFromFile("ClientConfiguration.xml");

            Console.ReadKey();

            while (true)
            {
                try
                {
                    GrainClient.Initialize(config);
                    Console.WriteLine("Connected to silo");
                    break;
                }
                catch (SiloUnavailableException)
                {
                    Console.WriteLine("Silo not available! Retrying in 3 seconds.");
                    Thread.Sleep(3000);
                }
            }

            ExplicitSubscriber();

            Console.ReadLine();
        }

        static void ExplicitSubscriber()
        {
            var subscriberGrain = GrainClient.GrainFactory.GetGrain<IExplicitSubscriberGrain>(Guid.Empty);
            var streamHandle = subscriberGrain.SubscribeAsync().Result;
            Console.WriteLine("Press enter to exit...");

            // 取消订阅
            Console.ReadLine();
            streamHandle.UnsubscribeAsync();
        }
    }
}
