﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host_Consul
{
    class Program
    {
        static void Main(string[] args)
        {
            //获得一个配置实例
            //它需要两个端口,第一个端口2234是用来silo与silo之间的通信的,第二个1234是用于监听client的请求的
            //var config = Orleans.Runtime.Configuration.ClusterConfiguration.LocalhostPrimarySilo(2234, 1234);

            //使用配置文件
            var config = new Orleans.Runtime.Configuration.ClusterConfiguration();
            config.LoadFromFile("host.xml");

            //初始化一个silohost,这里使用了Orleans提供的silohost而不是silo,其中silo的名字命名为Ba;
            Orleans.Runtime.Host.SiloHost silohost = new Orleans.Runtime.Host.SiloHost("Ba", config);

            //silohost.Config.Globals.LivenessType = GlobalConfiguration.LivenessProviderType.Custom;
            //silohost.Config.Globals.MembershipTableAssembly = "OrleansConsulUtils";
            //silohost.Config.Globals.ReminderServiceType = GlobalConfiguration.ReminderServiceProviderType.Disabled;

            silohost.InitializeOrleansSilo();

            silohost.StartOrleansSilo();

            //检查一下
            if (silohost.IsStarted)
            {
                Console.WriteLine("siloHost 启动成功");
            }
            else
            {
                Console.WriteLine("启动失败");
            }

            Console.ReadKey();

            // 关闭，停止
            silohost.StopOrleansSilo();
            silohost.UnInitializeOrleansSilo();
            silohost.ShutdownOrleansSilo();

        }
    }
}
