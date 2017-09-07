using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SiloHost";
            try
            {
                using (var siloHost = new SiloHost(Console.Title))
                {
                    siloHost.ConfigFileName = "OrleansConfiguration.xml";
                    siloHost.LoadOrleansConfig();

                    siloHost.InitializeOrleansSilo();
                    var startedOk = siloHost.StartOrleansSilo();

                    //检查一下
                    if (siloHost.IsStarted)
                    {
                        Console.WriteLine("Silo started successfully");
                    }
                    else
                    {
                        Console.WriteLine("启动失败");
                    }

                    Console.WriteLine("Press enter to exit...");
                    Console.ReadLine();
                    siloHost.ShutdownOrleansSilo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
