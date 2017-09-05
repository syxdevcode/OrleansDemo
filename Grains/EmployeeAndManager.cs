using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using IGrains;
using Orleans.Concurrency;

namespace Grains
{
    /// <summary>
    /// 为了 应对这样的死锁,Orleans提供了一个特性[Reentrant],
    /// 它可以标志一个Grain,使得这个类在处理其他消息的时候,稍微富有”弹性”,
    /// 可以同时接受其他新消息的到达,(只是”到达”这个步骤”弹性”化,”处理消息”
    /// 这个步骤依然是刚性的”单线程约束”)
    /// </summary>
    [Reentrant]
    public class Employee : Grain, IEmployee
    {
        public Task<int> GetLevel()
        {
            return Task.FromResult(_level);
        }
        public Task Promote(int newLevel)
        {
            _level = newLevel;
            return Task.CompletedTask;
        }
        public Task<IManager> GetManager()
        {
            return Task.FromResult(_manager);
        }
        public Task SetManager(IManager manager)
        {
            _manager = manager;
            return Task.CompletedTask;
        }

        public Task Greeting(IEmployee from, string message)
        {
            Console.WriteLine("{0} said: {1}", from.GetPrimaryKey().ToString(), message);
            return Task.CompletedTask;
        }

        // 死锁
        public async Task Greeting(GreetingData data)
        {
            Console.WriteLine("{0} said: {1}", data.From, data.Message);

            // stop this from repeating endlessly
            if (data.Count >= 3) return;

            // send a message back to the sender
            var fromGrain = GrainFactory.GetGrain<IEmployee>(data.From);
            await fromGrain.Greeting(new GreetingData()
            {
                From = this.GetPrimaryKey(),
                Message = "Thanks!",
                Count = data.Count + 1
            });
        }

        private int _level;
        private IManager _manager;
    }

    public class Manager : Grain, IManager
    {
        public override Task OnActivateAsync()
        {
            //获取和自己有一样标识的一个IEmployee类来代表自己,注意再Grain调用getGrain的方法.
            _me = this.GrainFactory.GetGrain<IEmployee>(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }
        public Task<List<IEmployee>> GetDirectReports()
        {
            return Task.FromResult(_reports);
        }
        public async Task AddDirectReport(IEmployee employee)
        {
            _reports.Add(employee);
            await employee.SetManager(this);
            //await employee.Greeting(_me, "Welcome to my team!");//发送給新人一个问候

            //死锁
            /*
            await employee.Greeting(new GreetingData
            {
                From = this.GetPrimaryKey(),
                Message = "Welcome to my team!"
            });
            */

            return;
        }
        public Task<IEmployee> AsEmployee()
        {
            return Task.FromResult(_me);
        }
        private IEmployee _me;
        private List<IEmployee> _reports = new List<IEmployee>();
        public string name;
    }
}
