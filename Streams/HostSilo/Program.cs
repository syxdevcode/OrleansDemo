using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime.Host;

namespace Silo_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SiloHost";
            try
            {
                using(var siloHost=new SiloHost(Console.Title))
                {
                    siloHost.LoadOrleansConfig();
                    siloHost.InitializeOrleansSilo();
                    var starteOk = siloHost.StartOrleansSilo();

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

                    // 关闭
                    siloHost.StopOrleansSilo();
                    siloHost.UnInitializeOrleansSilo();
                    siloHost.ShutdownOrleansSilo();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
