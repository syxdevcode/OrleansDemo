using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace IGrains
{
    public interface IEmployee : IGrainWithGuidKey
    {
        //获得员工的等级.
        Task<int> GetLevel();

        //提升员工的等级
        Task Promote(int newLevel);

        //获得管理员工的经理
        Task<IManager> GetManager();

        //设置此员工归属的经理
        Task SetManager(IManager manager);

        //表示某个员工向自己发送了一个问候
        Task Greeting(IEmployee from, string message);

        Task Greeting(GreetingData data);
    }

    public interface IManager : IGrainWithGuidKey
    {
        //经理也可以是员工
        Task<IEmployee> AsEmployee();

        //获得经理的直属员工
        Task<List<IEmployee>> GetDirectReports();

        //把员工加入到自己的直属团队
        Task AddDirectReport(IEmployee employee);
    }

    [Immutable]
    public class GreetingData
    {
        public Guid From { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
