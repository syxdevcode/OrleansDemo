using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //等待服务端启动完毕
            Console.ReadKey();
            //然后我聪明的敲击了回车键
            Run();
            Console.ReadLine();
        }

        static async void Run()
        {
            //利用内置方法获得一个配置类,这个类指明服务端的端口是1234
            //可以利用配置文件,不过这里我就先用这个简单的配置类
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(1234);

            // 初始化一个GrainClient
            GrainClient.Initialize(config);

            // 从silo处，获得一个BasicGrain的接口
            IGrains.IBasic agrain = GrainClient.GrainFactory.GetGrain<IGrains.IBasic>(314);
            IGrains.IBasic agrain2 = GrainClient.GrainFactory.GetGrain<IGrains.IBasic>(315);

            var result1 = agrain.DelayedMsg("agrain");
            //agrain2.SayHello("agrain2");
            Console.WriteLine(result1.Result);

            //agrain2.SayHello("315出发");

            //调用里面的方法，等待它返回
            string result = agrain.SayHello("可以").Result;
            Console.WriteLine(result);

            var grain = GrainClient.GrainFactory.GetGrain<IGrains.IExampleGrain>(0, "a string!","");
            grain.Hello();
        }
    }
}
